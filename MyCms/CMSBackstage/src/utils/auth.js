//定义常量
const TokenKey = "accessToken";
const RefreshTokenKey = "refreshToken"
const Username ="username"
const userRoleId ="userRoleId"
const Id ="id"


//获取用户id
export function getUserId() {
    return localStorage.getItem(Id) || '';
}
//获取用户角色id
export function getUserRoleId() {
    return localStorage.getItem(userRoleId) || '';
}
//获取Token
export function getToken() {
    return localStorage.getItem(TokenKey) || '';
}

//获取RefreshTokenKey
export function getRefreshToken() {
    return localStorage.getItem(RefreshTokenKey) || '';
}

//拿取登录的用户名
export function getUsername(){ 
    return localStorage.getItem(Username) || '';
}

//保存Token
export function SetToken(token,refreshToken) {

    localStorage.setItem(TokenKey,token);
    localStorage.setItem(RefreshTokenKey,refreshToken);
}

//清除登录的用户名
// export function clearUsername(){  
//     return localStorage.removeItem(Username);
// }

//注销登录
export function LogOut(){
    localStorage.removeItem(Id);
    localStorage.removeItem(userRoleId);
    localStorage.removeItem(Username);
    localStorage.removeItem(TokenKey);
    localStorage.removeItem(RefreshTokenKey);
}

//判断是否为登录状态
export function IsLogin() {
    let token = getToken();
    if(token){
        return true;
    }else{
        return false
    }

}



