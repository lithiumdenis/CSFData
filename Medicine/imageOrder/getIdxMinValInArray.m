function [ idxs ] = getIdxMinValInArray( arr, arrsize )

minval = arr(1,1);
idxs = 1;

for i = 1:arrsize
    if(arr(i,1) < minval)
        minval = arr(i,1);
        idxs = i;
    end
end