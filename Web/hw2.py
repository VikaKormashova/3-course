class Account:
    def __init__(self, account_number: str, balance: float, currency: str):
        self.account_number: str = account_number
        self.balance: float = balance
        self.currency: str = currency
    
    def __str__(self) -> str:
        return f"{self.account_number}: {self.balance} {self.currency}"


class Client:
    def __init__(self, passport_data: str, credit_rating: int):
        self.passport_data: str = passport_data
        self.credit_rating: int = credit_rating
        self.accounts: list[Account] = []
    
    def add_account(self, account: Account) -> None:
        if account not in self.accounts:
            self.accounts.append(account)
    
    def remove_account(self, account: Account) -> None:
        if account in self.accounts:
            self.accounts.remove(account)
            
    def count_of_accounts(self) -> int:
        return len(self.accounts)
    
    def get_account(self, index: int) -> Account:
        if 0 <= index < len(self.accounts):
            return self.accounts[index]
        else:
            raise IndexError()

    def get_accounts(self) -> list[Account]:
        return list(self.accounts)
    
    def __str__(self) -> str:
        return f"{self.passport_data}, рейтинг: {self.credit_rating}"


class Bank:
    def __init__(self, name: str):
        self.name: str = name
        self.total_profit: float = 0.0
        self.clients: list[Client] = []
        
    def add_client(self, client: Client) -> None:
        if client not in self.clients:
            self.clients.append(client)
            
    def add_profit(self, amount: float) -> None:
        if amount > 0:
            self.total_profit += amount
            
    def remove_client(self, client: Client) -> None:
        if client in self.clients:
            self.clients.remove(client)
            
    def get_clients(self) -> list[Client]:
        return list(self.clients)

    def get_bank_info(self) -> str:
        lines = [f"Банк: {self.name}", f"Общая прибыль: {self.total_profit}", "Клиенты:"]

        for client in self.clients:
            lines.append(f"  {client.passport_data} (рейтинг: {client.credit_rating}):")
            for account in client.accounts:
                lines.append(f"    {account}")

        return "\n".join(lines)
        
    
    def __str__(self) -> str:
        return self.name


def main():
    client1 = Client('4500 123456', 85)
    client2 = Client('4500 789012', 72)
    
    account1 = Account('40817810099910004321', 15000.0, 'RUB')
    account2 = Account('40817810099910005678', 5000.0, 'RUB')
    account3 = Account('40817810099910009876', 1000.0, 'USD')
    
    client1.add_account(account1)
    client1.add_account(account3)
    client2.add_account(account2)
    
    bank = Bank('Сбербанк')
    bank.add_client(client1)
    bank.add_client(client2)
    bank.add_profit(100000.0)
    
    print(bank.get_bank_info())


if __name__ == "__main__":
    main()