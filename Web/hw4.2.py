from functools import wraps
import time

def retry(times=3, exceptions=(Exception,), delay=0.0):
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            last_exception = None
            for attempt in range(times):
                try:
                    return func(*args, **kwargs)
                except exceptions as e:
                    last_exception = e
                    if attempt < times - 1 and delay > 0:
                        time.sleep(delay)
            raise last_exception
        return wrapper
    return decorator

if __name__ == "__main__":
    i = 0
    
    @retry(times=4, exceptions=(ValueError,), delay=0.05)
    def flaky():
        global i
        i += 1
        if i < 3:
            raise ValueError("not yet")
        return "ok"
    
    print(flaky())  