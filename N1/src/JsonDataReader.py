import json

from Types import DataType
from DataReader import DataReader


class JsonDataReader(DataReader):
    def read(self, path: str) -> DataType:
        students: DataType = {}

        with open(path, encoding='utf-8') as file:
            data: dict = json.load(file)

        for student_name, subjects in data.items():
            students[student_name] = []
            for name, grade in subjects.items():
                students[student_name].append((name, grade))

        return students
