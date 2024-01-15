from collections.abc import Iterator
from typing import Iterator
import requests

# TODO: Split header and actual data

class CsvError(Exception):
    def __self__(self, message):
        super().__init__(message)

class CsvRow:
    def __init__(self, args: list[any]):
        self.cells = args
        self.count = len(args)

        self._internal_count = 0
    
    def __iter__(self):
        return self
    
    def __next__(self):
        if self._internal_count == self.count:
            self._internal_count = 0
            raise StopIteration
        else:
            current = self.cells[self._internal_count]
            self._internal_count += 1
            return current
        
    def __getitem__(self, x):
        return self.cells[x]

class CsvTable:
    allowed = [";", ","]
    def __init__(self):    
        self.rowCount = 0
        self.columnCount = 0
        self.content: list[CsvRow] = []
        self.separation = None

        self._iter_count = 0

    def add_line(self, line: str) -> None:
        if self.separation is None:
            self.separation = self._get_separation_sign(line)
        if (self.columnCount == 0):
            columnCount = len(line.split(self.separation))
            if (columnCount == 0):
                raise CsvError("Csv has no content!")
        # TODO: analyse the line to make sure it just contains separator of <separation>
        if not self._is_correct_separactor(line):
            raise CsvError(f"Detected incorrect csv separator at line {self.rowCount + 1}")
        content = [i.strip() for i in line.split(self.separation)]
        self.content.append(CsvRow(content))
        self.rowCount += 1

    def _get_separation_sign(self, line) -> str: 
        analyseable = [i for i in line if i in CsvTable.allowed]
        return max(analyseable)
    
    def _is_correct_separactor(self, line) -> bool:
        return True
    
    def __iter__(self):
        return self

    def __next__(self): 
        if (self._iter_count == self.rowCount):
            self._iter_count = 0
            raise StopIteration
        else:
            row = self.content[self._iter_count]
            self._iter_count += 1
            return row
        
    def get_row(self, number) -> list:
        for elem in self.content[number - 1]:
            yield elem
        
    def get_column(self, number) -> list:
        for row in self.content:
            yield row[number - 1]

    # Accessing operator 
    def __getitem__(self, key):
        return self.content[key[0]][key[1]]

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
            table.add_line(line)
        return table
            
    @staticmethod 
    def read_from_inet(url: str) -> CsvTable | None:
        response = requests.get(url) 
        if response.status_code == 200:
            return CsvReader.read_from_string(response.content.decode())            
        return None
    
if __name__ == "__main__":
    CsvReader.read_from_file("classdefinition.csv")
    CsvReader.read_from_string("c1,c2,c3\r1,2,3\r1,2,3")
    CsvReader.read_from_string("c1,c2,c3\n1,2,3\n1,2,3\n")
    CsvReader.read_from_string("c1;c2;c3\n1;2;3\n1;2;3\n")

    table = CsvReader.read_from_inet("https://media.githubusercontent.com/media/datablist/sample-csv-files/main/files/organizations/organizations-100.csv")
    for item in table.get_column(4):
        print(item)