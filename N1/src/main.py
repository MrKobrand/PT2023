import argparse
import sys
from CalcRating import CalcRating
from SuccessRating import SuccessRating

from DataReader import DataReader
from TextDataReader import TextDataReader
from JsonDataReader import JsonDataReader


def get_path_from_arguments(args) -> str:
    parser = argparse.ArgumentParser(description='Path to datafile')

    parser.add_argument(
        '-p',
        dest='path',
        type=str,
        required=True,
        help='Path to datafile'
    )

    args = parser.parse_args(args)
    return args.path


def main():
    path = get_path_from_arguments(sys.argv[1:])

    reader: DataReader = None

    if path.endswith('.txt'):
        reader = TextDataReader()
    elif path.endswith('.json'):
        reader = JsonDataReader()
    else:
        raise ValueError('Incorrect file type provided!')

    students = reader.read(path)

    print('Students: ', students)

    calc_rating = CalcRating(students).calc()
    success_rating = SuccessRating(students).calc()

    print('Calc rating: ', calc_rating)
    print('Success rating: ', success_rating)

    successful_student_exists = False

    for student_name, success_info in success_rating.items():
        if success_info['Success']:
            successful_student_exists = True
            print('Successful student: ', student_name)
            break

    if not successful_student_exists:
        print('There are no successful students')


if __name__ == '__main__':
    main()
