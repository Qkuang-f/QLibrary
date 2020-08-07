


new Docute({
  
  layout: 'left',
  darkThemeToggler:true,
  imageZoom:true,
  // 插件
  
  // 上面：布局靠左、夜间模式按钮、图片点击
  sidebar: [
    // A sidebar item, with child links
    {
      title: '测试目录',
      children: [
        {
          title: '测试',
          link: '/Blog/Test/test.md'
        },
        {
          title: '测试二',
          link: '/Blog/Test/testHome.md'
        }
       
      ]
    },
    // An external link
    {
      title: 'Gitee',
      link: 'https://gitee.com/qkuang'
    }
  ]
})