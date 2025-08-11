// 登录拦截脚本，适用于 Razor Components (.NET 8) 客户端渲染后执行
window.addEventListener('DOMContentLoaded', function () {
    var isLogin = localStorage.getItem('isLogin');
    var path = location.pathname.toLowerCase();
    // 只在首次进入页面时拦截，跳转后不再干扰 SPA 路由
    if (isLogin !== 'true' && !path.startsWith('/login') && !path.startsWith('/register') && !path.startsWith('/forgotpassword')) {
        if (!window._loginRedirected) {
            window._loginRedirected = true;
            location.replace('/login');
        }
    }
});
