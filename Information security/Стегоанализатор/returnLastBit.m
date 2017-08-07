function result = returnLastBit( decInputVar )
%returnLastBit return last bit
binInputVar = dec2bin(decInputVar);
binInputVar = binInputVar-'0'; %convert bin to vector
if binInputVar(end) == 0
    result = 0;
else
    result = 1;
end