from typing import Union
from Types import DataType

SuccessType = dict[str, dict[str, Union[bool, dict[str, float]]]]


class SuccessRating:
    def __init__(self, data: DataType) -> None:
        self.data: DataType = data
        self.SUCCESS_SUBJECT_GRADE = 90
        self.SUCCESS_SUBJECTS_COUNT = 2

    def calc(self) -> SuccessType:
        success: SuccessType = {}
        success_subjects_key: str = 'Success subjects'
        failed_subjects_key: str = 'Failed subjects'

        for student_name, subjects in self.data.items():
            success[student_name] = {}
            success[student_name][success_subjects_key] = {}
            success[student_name][failed_subjects_key] = {}

            for name, grade in subjects:
                subjects_key = \
                    success_subjects_key \
                    if grade >= self.SUCCESS_SUBJECT_GRADE else \
                    failed_subjects_key

                success[student_name][subjects_key][name] = grade

            success[student_name]['Success'] = \
                len(success[student_name][success_subjects_key]) \
                >= self.SUCCESS_SUBJECTS_COUNT

        return success
