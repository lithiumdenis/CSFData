function [ dist ] = getDist( im1, im2 )
%Get Hamming distance between pair of images

%im1 = rgb2ntsc(imread(im1name));
%im2 = rgb2ntsc(imread(im2name));

%im1 = im1(:,:,1);
%im2 = im2(:,:,1);

D = pdist2(im1,im2,'hamming');
dist = sum(sum(D));

end

