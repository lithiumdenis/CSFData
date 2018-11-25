def bubble_sort(some_list):

    is_sorted = False
    last_sorted_item = len(some_list) - 1

    while not is_sorted:

        is_sorted = True

        for i in range(0, last_sorted_item):

            if some_list[i] > some_list[i + 1]:

                some_list[i], some_list[i+1] = some_list[i+1], some_list[i]
                is_sorted = False

        last_sorted_item -= 1
    
    return some_list
    

my_numbers = [92,11,45,2234,0,7,65]
print(bubble_sort(my_numbers))