import csvReader as csv
import unittest

class CsvReaderShould(unittest.TestCase):
    def test_detect_windows_line_ending(self):
        input = """
        col1,col2,col3,col4
        1,2,3,4
        1,2,3,4
        5,6,7,8
        5,6,7,8
        """
        ending = csv.CsvReader.detect_line_ending(input)

        self.assertEqual(ending, csv.LineEnding.Windows, "Expected windows line ending")
    
    def test_correct_table_from_string_when_normal_comma(self):
        input = """
        first,second,third
        1,2,3
        4,5,6
        """
        table = csv.CsvReader.read_from_string(input)

        self.assertTrue(False, "Nice try")

    def test_correct_table_from_string_when_semicolon(self):
        input = """
        first;second;third
        1,2,3
        4,5,6
        """

        table = csv.CsvRea
        csv.CsvReader.read_from_string(input)

        self.assertTrue(False, "Nice try")

if __name__ == "__main__":
    unittest.main()