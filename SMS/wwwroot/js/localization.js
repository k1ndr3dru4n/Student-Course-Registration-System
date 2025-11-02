// 本地化JavaScript辅助函数
window.localizationHelper = {
    // 从URL获取culture参数
    getCultureFromUrl: function() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('culture') || 'zh-CN';
    },

    // 本地化文本映射
    localizedTexts: {
        'zh-CN': {
            'SystemTitle': '学生管理系统',
            'WelcomeBack': '欢迎回来',
            'WelcomeToSystem': '欢迎使用学生管理系统！',
            'SystemDescription': '本系统集成了学生、教师、课程、选课、成绩等多项管理功能，支持数据统计、信息检索、权限管理等操作。用户可通过左侧导航栏快速访问各类管理页面，实现高效的教务管理与信息查询。',
            'SystemOverview': '系统数据概览',
            'StudentCount': '学生总数',
            'TeacherCount': '教师总数',
            'CourseCount': '课程总数',
            'EnrollmentCount': '选课记录',
            'Loading': '正在加载数据...',
            'AuthenticationFailed': '认证失败',
            'AuthDescription': '您没有权限访问此页面或登录已过期，正在跳转到登录页面...',
            'LoadingError': '加载数据时出现错误'
        },
        'en-US': {
            'SystemTitle': 'Student Management System',
            'WelcomeBack': 'Welcome back',
            'WelcomeToSystem': 'Welcome to the Student Management System!',
            'SystemDescription': 'This system integrates multiple management functions for students, teachers, courses, enrollments, grades, etc. It supports data statistics, information retrieval, permission management and other operations. Users can quickly access various management pages through the left navigation bar to achieve efficient educational administration and information query.',
            'SystemOverview': 'System Data Overview',
            'StudentCount': 'Total Students',
            'TeacherCount': 'Total Teachers', 
            'CourseCount': 'Total Courses',
            'EnrollmentCount': 'Enrollment Records',
            'Loading': 'Loading data...',
            'AuthenticationFailed': 'Authentication Failed',
            'AuthDescription': 'You do not have permission to access this page or your login has expired. Redirecting to login page...',
            'LoadingError': 'Error occurred while loading data'
        }
    },

    // 获取本地化文本
    getText: function(key) {
        const culture = this.getCultureFromUrl();
        return this.localizedTexts[culture]?.[key] || this.localizedTexts['zh-CN'][key] || key;
    },

    // 更新页面标题
    updatePageTitle: function() {
        const culture = this.getCultureFromUrl();
        const systemTitle = this.getText('SystemTitle');
        document.title = `Home - ${systemTitle}`;
    }
};

// 页面加载时自动更新标题
document.addEventListener('DOMContentLoaded', function() {
    if (window.localizationHelper) {
        window.localizationHelper.updatePageTitle();
    }
});
