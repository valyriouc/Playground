from typing import TypeVar, Generic, Sequence

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
        self.isReset = False
        self.ring: list[T] = [None] * capacity

    def read(self) -> T:
        if ((self.readPointer + 1) == self.capacity):
            self.isReset = False
            self.readPointer = -1
        if ((self.readPointer + 1) > self.writePointer and not self.isReset):
            # This is the problem when going down to start 
            raise OperationError() 
        self.readPointer += 1
        value = self.ring[self.readPointer]
        return value
    
    def write(self, value: T) -> None:
        if ((self.writePointer + 1) == self.capacity):
            self.isReset = True
            self.writePointer = -1
        self.writePointer += 1
        self.ring[self.writePointer] = value

if __name__ == "__main__":
    buffer = RingBuffer[int](4)

    buffer.write(100)
    buffer.write(200)

    r1 = buffer.read()
    print(r1)
    buffer.write(300)

    r2 = buffer.read()
    print(r2)

    buffer.write(400)
    buffer.write(500)

    r3 = buffer.read()
    print(r3)

    r4 = buffer.read()
    print(r4)

    r5 = buffer.read()
    print(r5)