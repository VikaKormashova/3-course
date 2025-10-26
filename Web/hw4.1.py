from functools import wraps

def retry3(func):
    @wraps(func)
    def wrapper(*args, **kwargs):
        last_exception = None
        for _ in range(3):
            try:
                return func(*args, **kwargs)
            except Exception as e:
                last_exception = e
        raise last_exception
    return wrapper

if __name__ == "__main__":
    i = 0
    
    @retry3
    def flaky():
        global i
        i += 1
        if i < 3:
            raise ValueError("fail")
        return "success"
    
    print(flaky())  