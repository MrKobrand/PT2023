import pytest
from src.Types import DataType
from src.JsonDataReader import JsonDataReader


class TestJsonDataReader:
    @pytest.fixture()
    def file_and_data_content(self) -> tuple[str, DataType]:
        text = """{
            "Иванов Иван Иванович": {
                "Математика": 67,
                "Биология": 66,
                "Программирование": 91
            },
            "Петров Петр Петрович": {
                "Математика": 78,
                "Химия": 87,
                "Социология": 61
            }
        }"""

        data = {
            'Иванов Иван Иванович': [
                ('Математика', 67), ('Биология', 66), ('Программирование', 91)
            ],
            'Петров Петр Петрович': [
                ('Математика', 78), ('Химия', 87), ('Социология', 61)
            ]
        }

        return text, data

    @pytest.fixture()
    def filepath_and_data(
            self,
            file_and_data_content: tuple[str, DataType],
            tmpdir) -> tuple[str, DataType]:

        p = tmpdir.mkdir('datadir').join('my_data.txt')
        p.write_text(file_and_data_content[0], encoding='utf-8')
        return str(p), file_and_data_content[1]

    def test_read(self, filepath_and_data: tuple[str, DataType]) -> None:
        file_content = JsonDataReader().read(filepath_and_data[0])
        assert file_content == filepath_and_data[1]
