function [ ] = CodeDiff( currname )
    

    %currname = 'Блок 1';
    addpath(currname);
    
    list = ls(strcat(currname, '/*.*g'));
    %Число изображений
    lsize = size(list);

    rasst = zeros(lsize(1), lsize(1));

    % get RGB images from files
    imsrgb = cell(1, lsize(1));
    for i = 1:lsize(1)
        imsrgb{1, i} = imread(strtrim(list(i,:)));
    end

    % get 1D images from files
    ims = cell(1, lsize(1));
    for i = 1:lsize(1)
        ims{1, i} = rgb2ntsc(imread(strtrim(list(i,:))));
        ims{1, i} = ims{1, i}(:,:,1);
    end

    % get hamming distances
    for i = 1:lsize(1)
        for j = 1:lsize(1)

            %tic;

            if(i ~= j)
                rasst(i, j) = getDist(ims{1, i}, ims{1, j});
            else
                rasst(i, j) = 100000000000; %not minimum
            end

            %time = toc;
            %fprintf(1, 'Этап: %d (из %d). Время выполнения: %g секунд\n', ((i - 1) * lsize(1)) + j, lsize(1) * lsize(1), time);
        end
    end

    % Какой после какого (idx - текущий, value(idx) - следующий)
    ttt = zeros(2,20);
    M = rasst;

    for i = 1:lsize(1)
        ttt(1,i) = getIdxMinValInArray(M(:,i), lsize(1));
        ttt(2,i) = i;
        for k = 1:lsize(1)
            M(ttt(1,i), k) = 100000000000; %not minimum if minimum at this idx
        end
    end

    % Порядок изображений
    order = zeros(1,20);
    order(1,1) = ttt(1,1); % мб цикл
    for k = 2:lsize(1)

        order(1,k) = ttt(1,order(1,k-1));
    end

    %Запись
    for i = 1:lsize(1)
        imwrite(imsrgb{1, order(1,i)}, strtrim(list(i,:)), 'png');

    end

end
