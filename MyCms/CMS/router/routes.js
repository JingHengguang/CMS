//引入中间件
// import Home from '../src/components/Home'
// import Artist from '../src/components/Artist'
// import Layout from '../src/components/Layout'

let routes = [
    {
        path: '/', 
        redirect:{path: '/index/detailed'},
        title: '根目录', 
        // component: Layout,
    },
    {
        path: '/index',
        redirect: { path: '/index/detailed' },
        meta: {
            title: '首页',
            icon: 'el-icon-add-location',
            hidden: false,
        },
        component: () => import('../src/views/layout.vue'),
        children: [
            {
                path: 'detailed',
                meta: {
                    title: '首页',
                    icon: 'el-icon-location-outline',
                    hidden:true
                },
                components: {
                    detailed: () =>
                        import('../src/views/index.vue'),
                    dynamic: () =>
                        import('../src/views/dynamic.vue'),
                    mien: () =>
                        import('../src/views/mien.vue'),
                    about: () =>
                        import('../src/views/about.vue'),
                },
            }, 
            {
                path: 'detailed',
                meta: {
                    title: '首页',
                    icon: 'el-icon-location-outline',
                    hidden:true
                },
            },
            {
                path: 'news',   
                meta: {
                    title: '最新文章',
                    icon: 'el-icon-location-outline',
                    hidden:true
                },
                component: () => import('../src/views/news.vue')               
            },
            {
                path: 'readrecommend',   
                meta: {
                    title: '阅读推荐',
                    icon: 'el-icon-location-outline',
                    hidden:true

                },
                component: () => import('../src/views/readrecommend.vue')               
            },
            {
                path: 'hourarticle',   
                meta: {
                    title: '48hour内文章',
                    icon: 'el-icon-location-outline',
                    hidden:true

                },
                component: () => import('../src/views/hourarticle.vue')               
            },
    
        ]
    },
    // {
    //     path: '/login',
    //     meta: {
    //         title: '登录',
    //         icon: 'el-icon-add-location',
    //         hidden: true
    //     },
    //     component: () => import('../src/views/login.vue'),

    // },
    //注册路由
    {
        path: '/register',
        meta: {
            title: '注册',
            icon: 'el-icon-add-location',
            hidden: true
        },
        component: () => import('../src/views/register.vue'),

    },
    {
        path: '/test',
        meta: {
            title: '首页',
            icon: 'el-icon-add-location',
            hidden: true
        },
        component: () => import('../src/views/test.vue'),
    },
  
]

export default routes
