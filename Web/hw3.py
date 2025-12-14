class Animal:
    def __init__(self, species: str, weight: float):
        self.species = species
        self.weight = weight
        
    def description(self) -> str:
        return f"Это {self.species}, массой {self.weight} кг."


class Dog(Animal):
    def __init__(self, weight: float, hair_length: str):
        super().__init__("собака", weight)
        self.hair_length = hair_length
    
    def description(self) -> str:
        return f"Это {self.species}, массой {self.weight} кг. Длина шерсти - {self.hair_length}."


class Cat(Animal):
    def __init__(self, weight: float, color: str):
        super().__init__("кот", weight)
        self.color = color
    
    def description(self) -> str:
        return f"Это {self.species}, массой {self.weight} кг. Окрас кота — {self.color}."


# Создание экземпляров и вывод описаний
dog = Dog(15.0, "средняя")
cat = Cat(5.0, "рыжий")

print(dog.description())
print(cat.description())