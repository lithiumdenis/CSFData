//Функция линейно отображающая x из диапазона [xmin..xmax] в диапазон [ymin..ymax] для y 
function Project(x,xmin,xmax,ymin,ymax){
  return (ymin + (x - xmin)*(ymax - ymin)/(xmax - xmin));
}

//Цвет
function RGB(r,g,b){
  var decColor = 0x1000000 + Math.round(b) + 0x100 * Math.round(g) + 0x10000 * Math.round(r);
  return '#'+decColor.toString(16).substr(1);
}

//Случайное число
function Random(min, max){
  return Math.random() * (max - min) + min;
}

// Объявления глобальных переменных
var Xsize,  //Число точек на направлении X
    Ysize,  //Число точек на направлении Y
	Zsize,  //Число реагентов
	dt,     //Шаг по времени
	A,      // dt*K/(h*h)
	B,      // 1 - 4*A
	time,   //Время модели
	ds,     //Шаг по пространству. Шаг по X равен шагу по Y.
	kdiff,  //Коэффициенты диффузии реагентов
    fmin,   //Минимальное значение каждой функции (концентрации реагентов)
    fmax,   //Максимальное значение каждой функции (концентрации реагентов)
	cl,     //Три компоненты цвета пикселя RGB (R=U[0], G=U[2], B=U[1])
	xlenght,//Протяженность области по X
	ylenght,//Протяженность области по Y
	cxsize, //Размер графического контекста X
	cysize, //Размер графического контекста Y
    radius, //Размер изображаемой точки среды
	delay,  //Временная задержка между перерисовками картинки
    count,  //Число итераций расчета без перерисовки
	iter,   //Номер текущей итерации без перерисовки
    U,      //Рабочий массив для расчета диффузии [y][x][z]
    W,      //Вспомогательный массив для расчета диффузии [y][x][z]
	ic,     //Начальные условия (точнее точка равновесия, если есть)
    context,//Указатель на графический контекст
    drawing,//
    doing,  //Флаг осуществления расчета
	start,  //Флаг первого пуска
    timerId;//Указатель на таймер


function Inite(){
// Инициализация глобальных переменных
    cxsize   = 500;  //Размер графического контекста X
	cysize   = 500;  //Размер графического контекста Y
    Xsize    = 100;  //Число точек на направлении X
    Ysize    = 100;  //Число точек на направлении Y
	Zsize    = 2;    //Число реагентов
	dt       = 0.01;//Шаг по времени
	ds       = 0.04; //Шаг по пространству. Шаг по X равен шагу по Y.
	time     = 0.0;
    fmin     = new Array(Zsize);
    fmax     = new Array(Zsize);
	kdiff    = new Array(Zsize);
	A        = new Array(Zsize);
	B        = new Array(Zsize);
	ic       = new Array(Zsize);
	ic[0]    = 1.0;
	ic[1]    = 3.0;
	cl       = [0.0,0.0,0.0];
	kdiff[0] = 0.002;
	kdiff[1] = 0.001;
    fmin[0]  = 0.0;
	fmax[0]  = 7.0;
	fmin[1]  = 0.0;
	fmax[1]  = 7.0;
    xlenght  = ds*Xsize;
	ylenght  = ds*Ysize;
    radius   = Math.min(cxsize,cysize)/Math.max(Xsize,Ysize);
	delay    = 0;
    count    = 10;
	iter     = 0;
    doing    = false;
	start    = true;
	var dr;
	for (var n = 0; n < Zsize; n++){
	  dr = (ds * ds) / (4.0 * kdiff[n]);
	  dt = Math.min(dt,dr);
	}
	for (var n = 0; n < Zsize; n++){
	  A[n] = dt * kdiff[n] / (ds * ds);
	  B[n] = 1.0 - 4.0 * A[n];
	}
}

//Брюсселятор
function Brusselator(t,x,y){
  var a = 1.0, b = 3.0;
  W[y][x][0] = a - U[y][x][0]*(b - U[y][x][0]*U[y][x][1] + 1.0); //a - x * (b - x*y + 1)
  W[y][x][1] =     U[y][x][0]*(b - U[y][x][0]*U[y][x][1]); //x*(b-x*y)
}

function InitSpace(x,y,z){
  U = new Array(y);
  W = new Array(y);
  for (var i = 0; i < y; i++){
    U[i] = new Array(x);
	W[i] = new Array(x);
  }
  for (var i = 0; i < y; i++){
    for (var j = 0; j < x; j++){
	  U[i][j] = new Array(z);
	  W[i][j] = new Array(z);
	  
	  for (var n = 0; n < z; n++){
	    U[i][j][n] = ic[n];
	    if(Random(0,1000) < 1)//Управление заполнением пространства случайными значениями
	      U[i][j][n] = Random(fmin[n],fmax[n]);
	  }
    }//x
  }//y
}

Calc = function () {
  //Метод эйлера для каждой точки
  for (var y = 0; y < Ysize; y++){//Циклы по
    for (var x = 0; x < Xsize; x++){//пространственным переменным
	  Brusselator(time,x,y);
    }
  }
  
  //Метод конечных разностей (явная схема)
  for (var n = 0; n < Zsize; n++){//Цикл по реагентам
    for (var y = 0; y < Ysize; y++){//Циклы по
      for (var x = 0; x < Xsize; x++){//пространственным переменным
	    if(x == 0 && y == 0){
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][Xsize-1][n] + U[y+1][x][n] + U[Ysize-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(x == 0 && y == Ysize-1){
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][Xsize-1][n] + U[0][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(x == Xsize-1 && y == 0){
	      U[y][x][n] = A[n]*(U[y][0][n] + U[y][x-1][n] + U[y+1][x][n] + U[Ysize-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(x == Xsize-1 && y == Ysize-1){
	      U[y][x][n] = A[n]*(U[y][0][n] + U[y][x-1][n] + U[0][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(x == 0){
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][Xsize-1][n] + U[y+1][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(x == Xsize-1){
	      U[y][x][n] = A[n]*(U[y][0][n] + U[y][x-1][n] + U[y+1][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(y == 0){
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][x-1][n] + U[y+1][x][n] + U[Ysize-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	    if(y == Ysize-1){
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][x-1][n] + U[0][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
		} else
	      U[y][x][n] = A[n]*(U[y][x+1][n] + U[y][x-1][n] + U[y+1][x][n] + U[y-1][x][n]) 
		  + B[n]*U[y][x][n] + dt*W[y][x][n];
      }//x
    }//y
  }
  time += dt;
}

Draw = function () {
  
    for (var y = 0; y < Ysize; y++){
      for (var x = 0; x < Xsize; x++){
	    for (var n = 0; n < Zsize; n++){
		  cl[n] = Project(U[y][x][n],fmin[n],fmax[n],0,255);
		}
		context.fillStyle = RGB(cl[0],cl[2],cl[1]);
	    context.fillRect(x*radius,y*radius,radius,radius);
      }//x
    }//y

}

Proc = function () {
  if(doing){
    Calc();
	if(iter == 0){
      Draw();
	  iter = count;
    } else iter--;
    setTimeout('Proc()',delay);
  } else clearInterval(timerId);
}

Run = function () {
    if(start){
	  start = false;
      drawing = document.getElementById('PaintBox1');
      if(drawing && drawing.getContext){
        context = drawing.getContext('2d');
        drawing.height = cysize;
        drawing.width  = cxsize;
      }
	}
    timerId = setTimeout('Proc()',delay);
  doing = !doing;
}

function main() {
  Inite();
  InitSpace(Xsize,Ysize,Zsize);
}