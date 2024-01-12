from collections.abc import Iterator
from typing import Iterator
from io import TextIOWrapper

class CsvTable:
    def __init__(self):    
        self.headColumnNames = []
        self.rowCount = 0
        self.columnCount = 0
        self.content = []

    def add_line(self, line: str, isHead: bool = False):
        pass

class SplitAtLineEndIter:
    def __init__(self, input: str):
        self.buffer = input
    
    def __iter__(self):
        return self

    def __next__(self):
        if self.buffer == "":
            raise StopIteration
        elif("\n" not in self.buffer and "\r" not in self.buffer):
            res = self.buffer
            self.buffer = ""
            return res
        else:
            end, start = SplitAtLineEndIter._get_next_line(self.buffer)
            res = self.buffer[:end]
            self.buffer = self.buffer[start:]
            return res.strip()

    @staticmethod
    def _get_next_line(buffer: str) -> (int,int):
        index = 0
        while True:
            if (buffer[index] == "\r"):
                if (buffer[index+1] == "\n"):
                    return (index,index+2)
                else: 
                    return(index, index+1)
            if (buffer[index] == "\n"):
                return (index, index+1)
            index += 1

class CsvReader:
    def __init__(self):
        raise NotImplementedError("This class is a static one")

    @staticmethod
    def read_from_file(filename) -> CsvTable:
        with open(filename, "r") as fobj:
            input = fobj.read()
            return CsvReader.read_from_string(input)

    @staticmethod
    def read_from_string(csv: str) -> CsvTable:
        table = CsvTable()
        for line in SplitAtLineEndIter(csv):
            # table.add_line(line)
            print(line, end=" ")
        return table
            
    @staticmethod 
    def read_from_inet(url: str) -> CsvTable:
        pass

if __name__ == "__main__":
    CsvReader.read_from_file("classdefinition.csv")

    print()

    CsvReader.read_from_string("c1,c2,c3\r1,2,3\r1,2,3")

    print()

    CsvReader.read_from_string("c1,c2,c3\n1,2,3\n1,2,3\n")
