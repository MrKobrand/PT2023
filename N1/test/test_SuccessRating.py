import pytest

from src.Types import DataType
from src.SuccessRating import SuccessType
from src.SuccessRating import SuccessRating


class TestCalcRating:
    @pytest.fixture()
    def input_data(self) -> tuple[DataType, SuccessType]:
        data: DataType = {
            'Абрамов Петр Сергеевич': [
                ('Математика', 90),
                ('Русский язык', 76),
                ('Программирование', 100)
            ],
            'Петров Игорь Владимирович': [
                ('Математика', 61),
                ('Русский язык', 80),
                ('Программирование', 78)
            ]
        }

        success: SuccessType = {
            'Абрамов Петр Сергеевич': {
                'Success subjects': {
                    'Математика': 90,
                    'Программирование': 100
                },
                'Failed subjects': {
                    'Русский язык': 76
                },
                'Success': True
            },
            'Петров Игорь Владимирович': {
                'Success subjects': {},
                'Failed subjects': {
                    'Математика': 61,
                    'Русский язык': 80,
                    'Программирование': 78
                },
                'Success': False
            }
        }

        return data, success

    def test_init_success_rating(
            self,
            input_data: tuple[DataType, SuccessType]) -> None:
        success_rating = SuccessRating(input_data[0])
        assert input_data[0] == success_rating.data

    def test_success(self, input_data: tuple[DataType, SuccessType]) -> None:
        success = SuccessRating(input_data[0]).calc()

        for student_name, subjects_info in success.items():
            key = 'Success subjects'
            assert len(subjects_info[key]) \
                == len(input_data[1][student_name][key])

            for subjects_info_subject, input_data_subject \
                    in zip(
                        subjects_info[key].keys(),
                        input_data[1][student_name][key].keys()
                    ):
                assert subjects_info_subject == input_data_subject
                assert subjects_info[key][subjects_info_subject] \
                    == input_data[1][student_name][key][input_data_subject]

            key = 'Failed subjects'
            assert len(subjects_info[key]) \
                == len(input_data[1][student_name][key])

            for subjects_info_subject, input_data_subject \
                    in zip(
                        subjects_info[key].keys(),
                        input_data[1][student_name][key].keys()
                    ):
                assert subjects_info_subject == input_data_subject
                assert subjects_info[key][subjects_info_subject] \
                    == input_data[1][student_name][key][input_data_subject]

            key = 'Success'
            assert subjects_info[key] == input_data[1][student_name][key]
