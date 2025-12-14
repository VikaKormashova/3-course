class Subject:
    def __init__(self, name, teacher, count):
        self.name: str = name
        self.teacher: str = teacher
        self.count: int = count
    
    def __str__(self) -> str:
        return f'Предмет "{self.name}": преподаватель {self.teacher}, учащихся: {self.count}'
    
    def enroll(self):
        if self.count >= 0:
            self.count += 1
            print(f'На курс "{self.name}" зачислен новый учащийся. Всего: {self.count}')
        else:
            print("Ошибка: количество учащихся не может быть отрицательным")

def main():
    subject1 = Subject("Физика", "Александр Петрович", 10)
    print(subject1)
    subject1.enroll()
    print(subject1)

if __name__ == '__main__':
    main()