clc; clear;                                               
image = imread('Batterfly.png');                               
tic;                                                      
smallYiqInputImage = rgb2ntsc(image); 
countRedundancy = redundancyPatchesCount(smallYiqInputImage(:,:,1));  
fprintf(1,'���������� ������������� �������� = %g ����\n',countRedundancy);
toc;    