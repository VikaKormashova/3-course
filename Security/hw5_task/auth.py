from typing import Any, Tuple
import time  
import crypto
import user
import storage

def record_token(payload: dict[str, Any]) -> None:
    db = storage.load_tokens()
    db["tokens"].append({
        "jti": payload["jti"],
        "sub": payload["sub"],
        "typ": payload["typ"],
        "iat": payload["iat"],
        "exp": payload["exp"],
        "revoked": False
    })
    storage.save_tokens(db)

def revoke_by_jti(jti: str) -> None:
    db = storage.load_tokens()
    for token in db["tokens"]:
        if token["jti"] == jti:
            token["revoked"] = True
            break
    storage.save_tokens(db)

def is_revoked(jti: str) -> bool:
    db = storage.load_tokens()
    for token in db["tokens"]:
        if token["jti"] == jti:
            return token.get("revoked", False)
    return False

def is_expired(exp: int) -> bool:  
    return exp < int(time.time())

def login(username: str, password: str) -> Tuple[str, str]:
    u = user.get_user(username)
    if not u:
        raise ValueError("User not found")
    
    if not user.verify_password(u, password):
        raise ValueError("Invalid password")
    
    access_token, access_payload = crypto.issue_access(username)
    refresh_token, refresh_payload = crypto.issue_refresh(username)
    
    record_token(access_payload)
    record_token(refresh_payload)
    
    return access_token, refresh_token

def verify_access(access: str) -> dict[str, Any]:
    payload = crypto.decode(access)
    
    if payload.get("typ") != "access":
        raise ValueError("Wrong token type: expected access token")
    
    if is_revoked(payload["jti"]):
        raise ValueError("Token revoked")
    
    return payload

def refresh_pair(refresh_token: str) -> Tuple[str, str]:
    payload = crypto.decode(refresh_token)
    
    if payload.get("typ") != "refresh":
        raise ValueError("Wrong token type: expected refresh token")
    
    if is_revoked(payload["jti"]):
        raise ValueError("Refresh token revoked")
    
    revoke_by_jti(payload["jti"])
    
    username = payload["sub"]
    access_token, access_payload = crypto.issue_access(username)
    refresh_token, refresh_payload = crypto.issue_refresh(username)
    
    record_token(access_payload)
    record_token(refresh_payload)
    
    return access_token, refresh_token

def revoke(token: str) -> None:
    payload = crypto.decode(token)
    revoke_by_jti(payload["jti"])

def introspect(token: str) -> dict[str, Any]:
    try:
        payload = crypto.decode(token)
        active = (not is_revoked(payload["jti"])) and (not is_expired(payload["exp"]))
        return {
            "active": active,
            "sub": payload.get("sub"),
            "typ": payload.get("typ"),
            "exp": payload.get("exp"),
            "jti": payload.get("jti"),
        }
    except Exception:
        return {"active": False, "error": "invalid_token"}