from typing import TypeVar, Generic, Sequence, Callable

class IWalking:
    def __init__(self, speed, xPos, yPos):
        self.speed = speed
        self.xPos = xPos
        self.yPos = yPos

    def walk(self):
        raise NotImplementedError("Please implement the interface method!") 

class Walker(IWalking):
    def __init__(self):
        super().__init__(3, 1, 1)
    
    def walk(self):
        self.xPos += self.speed
        self.yPos += self.speed
        print("Walker was walking!")

class OperationError(Exception):
    def __init__(self):
        super().__init__("The operation is not allowed!")

T = TypeVar("T")
class RingBuffer(Generic[T]):
    def __init__(self, capacity):
        self.capacity = capacity
        self.readPointer = -1
        self.writePointer = -1
        self.ring: list[T] = [None] * capacity

    def read(self) -> T:
        if ((self.readPointer + 1) == self.capacity):
            self.readPointer = -1
        if ((self.readPointer + 1) > self.writePointer and 
            self.writePointer != 0):
            raise OperationError() 
        self.readPointer += 1
        value = self.ring[self.readPointer]
        return value
    
    def write(self, value: T) -> None:
        if ((self.writePointer + 1) == self.capacity):
            self.writePointer = -1
        self.writePointer += 1
        self.ring[self.writePointer] = value

def myfunc(val: int):
    print(val)

class FuncWrapper:
    def __init__(self, funcPtr: Callable[[int], None]):
        self.funcPtr = funcPtr
    
    def execute(self, val):
        self.funcPtr(val)

if __name__ == "__main__":
   
   wrapper = FuncWrapper(myfunc)

   wrapper.execute(200)