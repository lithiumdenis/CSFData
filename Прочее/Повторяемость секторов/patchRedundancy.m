clc; clear;                                               
image = imread('Batterfly.png');                               
tic;                                                      
smallYiqInputImage = rgb2ntsc(image); 
countRedundancy = redundancyPatchesCount(smallYiqInputImage(:,:,1));  
fprintf(1,'Количество повторяющихся секторов = %g штук\n',countRedundancy);
toc;    