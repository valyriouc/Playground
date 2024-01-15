from collections.abc import Iterator
from typing import Iterator, Callable
import requests

# TODO: Implementing type system for csv 

class CsvError(Exception):
    def __self__(self, message):
        super().__init__(message)

class CsvRow:
    def __init__(self, args: list[any], headers: dict):
        self.cells = args
        self.count = len(args)
        self.headers = headers
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
        
    def __getitem__(self, index: int | str):
        if type(index) is str:
            index = self.headers[index]
        return self.cells[index]
    
    def __str__(self):
        builder = ""
        first = True
        for i in self.cells:
            if first:
                builder += f"{i}"
                first = False
            else:
                builder += f",{i}"
        return builder

class CsvTable:
    allowed = [";", ","]
    def __init__(self):    
        self.rowCount = 0
        self.columnCount = 0
        self.content: list[CsvRow] = []
        self.headers = None
        self.separation = None

        self._iter_count = 0

    def add_line(self, line: str, hasHead: bool = False) -> None:
        if self.separation is None:
            self.separation = self._get_separation_sign(line)

        if not self._is_correct_separactor(line):
            raise CsvError(f"Detected incorrect csv separator at line {self.rowCount + 1}")
        columns = [cell.strip() for cell in line.split(self.separation)]

        if (self.columnCount == 0):
            columnCount = len(columns)
            if (columnCount == 0):
                raise CsvError("Csv has no content!")
        
        if (hasHead and self.headers is None):
            self.headers = {}
            for i in range(0, len(columns)):
                self.headers[columns[i]] = i
            return
        
        self.content.append(CsvRow(columns, self.headers))
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
        
    def get_row(self, index: int) -> list:
        for elem in self.content[index]:
            yield elem
        
    def get_row_where(self, func: Callable[[CsvRow], bool]) -> list[CsvRow]:
        for row in self.content:
            if (func(row)):
                yield row

    def get_first_or_default(self, func: Callable[[CsvRow], bool]) -> CsvRow | None:
        for row in self.content:
            if (func(row)):
                return row
        return None
    
    def get_max(self, func: Callable[[CsvRow], int]) -> CsvRow:
        current = None
        value = None
        for row in self.content:
            if (row is None):
                current = row
                value = func(row)
            else:
                tmp = func(row)
                if (tmp >= value):
                    current = row
                    value = tmp
        return current

    def get_column(self, index: str | int) -> list:
        if type(index) is str:
            print(self.headers)
            index = self.headers[index]
        for row in self.content:
            yield row[index]

    # Accessing operator 
    def __getitem__(self, key):
        if (type(key[1]) is str):
            index = self.headers[key[1]]
        return self.content[key[0]][index]

    def write_to_string(self) -> str:
        pass

    def write_to_file(self, filename) -> None:
        with open(filename, "w") as fobj:
            payload = self.write_to_string()
            fobj.write(payload)  

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
    def read_from_file(filename, hasHead: bool = False) -> CsvTable:
        with open(filename, "r") as fobj:
            input = fobj.read()
            return CsvReader.read_from_string(input, hasHead)

    @staticmethod
    def read_from_string(csv: str, hasHead: bool = False) -> CsvTable:
        table = CsvTable()
        for line in SplitAtLineEndIter(csv):
            table.add_line(line,hasHead)
        return table
            
    @staticmethod 
    def read_from_inet(url: str, hasHead: bool = False) -> CsvTable | None:
        response = requests.get(url) 
        if response.status_code == 200:
            decoded = response.content.decode()
            return CsvReader.read_from_string(decoded, hasHead)            
        return None
    
if __name__ == "__main__":
    table = CsvReader.read_from_file("classdefinition.csv", True)
    # CsvReader.read_from_string("c1,c2,c3\r1,2,3\r1,2,3", True)
    # CsvReader.read_from_string("c1,c2,c3\n1,2,3\n1,2,3\n", True)
    # table = CsvReader.read_from_string("col1;col2;col3\n1;2;3\n1;2;3\n", True)

    # table = CsvReader.read_from_inet(
    #     "https://media.githubusercontent.com/media/datablist/sample-csv-files/main/files/organizations/organizations-100.csv",
    #     True)
    
    print(table[1,"col3"])

    for row in table:
        print(row["col4"])

    res = table.get_row_where(lambda row: int(row["col1"]) == 1)
    for r in res:
        print(r)

    ro = table.get_first_or_default(lambda r: int(r["col4"]) == 4)
    print(ro)