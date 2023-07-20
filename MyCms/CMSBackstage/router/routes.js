//引入中间件
// import Home from '../src/components/Home'
// import Artist from '../src/components/Artist'
import Layout from '../src/components/Layout'

let routes = [
    {
        path: '/',
        component: Layout,
        title: '根目录',
        name: "layout",
        //将首页重定向到home界面
        //  redirect: { path: '/home' },
        meta: {
            title: '根目录',
            icon: 'el-icon-guide',
            hidden: true

        },
        children: [
            {
                path: 'home',
                title: '主页',
                meta: {
                    title: '主页',
                    icon: 'el-icon-news',
                    hidden: false
                },
                //异步加载主件
                component: () => import('../src/components/Home'),
            },        
        ]
    },
    {
        path: '/artist',
        component: Layout,
        name: 'artist',
        title: '文章信息',
        meta: {
            title: '文章信息',
            icon: 'el-icon-connection',
            hidden: false
        },
        children: [
            {
                path: 'types',
                name: "types",
                meta: {
                    title: '类型列表',
                    icon: 'el-icon-location-outline',
                    hidden: true
                },
                //引用一个CRUD用户界面
                component: () => import('../src/views/types')
            },
            {
                path: 'publish',
                title: '发布文章',
                meta: {
                    title: '发布文章',
                    icon: 'el-icon-location-outline',
                    hidden: false
                },
                //引用一个CRUD用户界面
                component: () => import('../src/views/publish')
            },
            {
                path: 'articleinfo',
                title: '文章信息',
                meta: {
                    title: '文章信息',
                    icon: 'el-icon-delete-location',
                    hidden: false
                },
                //引用一个CRUD用户界面
                component: () => import('../src/views/articleinfo')
            }

        ],
    },
    {
        path: '/system',
        component: Layout,
        name: 'system',
        title: '系统信息管理',
        meta: {
            title: '系统信息管理',
            icon: 'el-icon-connection',
            hidden: true,
        },
        children: [

            {
                path: 'user',
                title: '用户信息',
                name: 'user',
                meta: {
                    title: '用户信息',
                    icon: 'el-icon-location-outline',
                    hidden: true,
                },
                //引用一个CRUD用户界面
                component: () => import('../src/views/users')
            }
        ],
    },
    {
        path: '/log',
        component: Layout,
        redirect: { path: '/log/auditinfo' },
        meta: {
            title: '系统日志',
            icon: 'el-icon-coin',
            hidden: false
        },
        children: [
            {
                path: 'auditinfo',
                name: 'auditinfo',
                meta: {
                    title: '审计日志',
                    icon: 'el-icon-add-location',
                },
                component: () => import('../src/views/logs.vue'),
            },
        ],
    },
    {
        path: '/login',
        meta: {
            title: '登录',
            icon: 'el-icon-add-location',
            hidden: true
        },
        component: () => import('../src/views/login.vue'),

    },
    //注册路由
    // {
    //     path: '/register',
    //     meta: {
    //         title: '注册',
    //         icon: 'el-icon-add-location',
    //         hidden: true
    //     },
    //     component: () => import('../src/views/register.vue'),

    // },

    ///个人信息
    {
        path: '/userinfo',
        component: Layout,
        redirect: { path: '/userinfo/edituserinfo' },
        meta: {
            title: '个人信息',
            icon: 'el-icon-user',
            hidden: false
        },
        children: [
            {
                path: 'edituserinfo',
                name: 'edituserinfo',
                meta: {
                    title: '修改个人信息',
                    icon: 'el-icon-edit-outline',
                    hidden: false
                },
                component: () => import('../src/views/edituserinfo.vue'),
            },
            {
                path: 'editAvatar',
                name: 'editAvatar',
                meta: {
                    title: '修改头像',
                    icon: 'el-icon-edit-outline',
                    hidden: false
                },
                component: () => import('../src/views/editavatar.vue'),
            },
        ],
    },
    ///图片管理
    {
        path: '/pictureinfo',
        name: "pictureinfo",
        component: Layout,
        // redirect: { path: '/pictureinfo/edituserinfo'},
        meta: {
            title: '图片管理',
            icon: 'el-icon-user',
            hidden: false
        },
        children: [
            {
                path: 'rotationchartinfo',
                name: 'rotationchartinfo',
                meta: {
                    title: '轮播图管理',
                    icon: 'el-icon-edit-outline',
                    hidden: true
                },
                component: () => import('../src/views/rotationchartinfo.vue'),
            },
            {
                path: 'addrotationchart',
                name: 'addrotationchart',
                meta: {
                    title: '操作轮播图',
                    icon: 'el-icon-edit-outline',
                    hidden: true
                },
                component: () => import('../src/views/addrotationchart.vue'),
            },
        ],
    },
    ///日历
    {
        path: '/calendar',
        component: Layout,
        redirect: { path: '/calendar/calendarinfo' },
        meta: {
            title: '日历',
            icon: 'el-icon-coin',
            hidden: false
        },
        children: [
            {
                path: 'calendarinfo',
                name: 'calendarinfo',
                meta: {
                    title: '日历信息',
                    icon: 'el-icon-date',
                    hidden: false
                },
                component: () => import('../src/views/calendarinfo.vue'),
            },
        ],
    },
      //404
       {
        path: '*',
        name: "404",
        meta: {
            title: '错误界面',
            hidden: true
        },
        component: () => import('../src/views/error.vue'),
    },


]

export default routes
