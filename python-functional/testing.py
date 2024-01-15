import typing
import collections.abc as coll
import csv

T = typing.TypeVar("T", bound=typing.Sequence)
class ReverseIter(typing.Generic[T]):
    def __init__(self, iterable: T):
        self.iterable = iterable
        self.count = len(iterable) 

    def __iter__(self):
        return self
    
    def __next__(self):
        if (self.count == 0):
            raise StopIteration
        else:
            self.count -= 1
            return self.iterable[self.count]

class Testing:
    def __init__(self):
        pass

    def __iter__(self):
        return ReverseIter([1,2,3,4,5,6])


if __name__ == "__main__":
    # csv.DictReader()
    # for i in Testing():
    #     print(i)

    test = ["Hello", "1", "3.2", "True"]
    
    print(res)