﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="JsCommon_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>海报画廊</title>
    <style type="text/css">
      *{
        padding: 0;
        margin: 0;
      }
      body{
        background-color: #fff;
        color: #555;
        font-family: 'Avenir Next', 'Lantinghei SC';
        font-size: 14px;
        width:100%;
        height:100%;
        /*-webkit-font-smoothing：none | subpixel-antialiased | antialiased
         *none：对低像素的文本比较好
         *subpixel-antialiased：默认值
         *antialiased：抗锯齿
         *-moz-osx-font-smoothing是mozilla给特定操作系统推出的特性增强
         *设置grayscale对于图标字体表现更清晰，减轻次像素渲染带来的相邻像素色彩污染问题
         */
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
      }
      .wrap{
        width: 100%;
        height: 600px;
        position: absolute;
        top: 50%;
        margin-top: -300px;
        background-color: #333;
        overflow: hidden;

        /*perspective 是 CSS3 属性，目前浏览器都不支持 perspective 属性，
         *Chrome 和 Safari 支持替代的 -webkit-perspective 属性。
         *perspective 属性定义 3D 元素距视图的距离，以像素计，其值越小，用户与3D空间Z平面距离越近，视觉效果更令人印象深刻；
         *反之，值越大，用户与3D空间Z平面距离越远，视觉效果就很小
         *当为元素定义 perspective 属性时，其子元素会获得透视效果，而不是元素本身，perspective 属性只影响 3D 转换元素
         *与 perspective-origin 属性一同使用该属性，这样您就能够改变 3D 元素的底部位置
         *perspective: number | none; 
         *number：元素距离视图的距离，以像素计(px 可以不写)
         *none：默认值，与 0 相同，不设置透视
         *在3D变形中，除了perspective属性可以激活一个3D空间之外，在3D变形的函数中的perspective()也可以激活3D空间。
         *他们不同的地方是：perspective用在舞台元素上（变形元素们的共同父元素）；perspective()就是用在当前变形元素上，
         *并且可以与其他的transform函数一起使用。例如，我们可以把：
         *.stage {perspective: 600px;} 写成 .stage .box {transform: perspective(600px);}
         *perspective()函数取值只能大于0，如果取值为0或比0小的值，将无法激活3D空间
         */
        perspective: 800px;
        -webkit-perspective: 800px;
      }
      /* 海报样式 s*/
      .photo{
        position: absolute;
        width: 260px;
        height: 320px;
        z-index: 1;
        box-shadow: 0 0 1px rgba(0, 0, 0, .01);
        /*IE 10、Firefox、Opera 和 Chrome 支持 transition 属性
         *Safari 支持替代的 -webkit-transition 属性
         *Internet Explorer 9 以及更早版本的浏览器不支持 transition 属性
         *transition 属性是一个简写属性(默认值：all 0 ease 0)，用于设置四个过渡属性：
         *transition-property，定义应用过渡效果的 CSS 属性名称列表，列表以逗号分隔；all 则所有属性都将获得过渡效果
         *transition-duration，规定完成过渡效果需要多少秒或毫秒，必须始终设置 transition-duration 属性，否则时长为 0，就不会产生过渡效果
         *transition-timing-function，规定速度效果的速度曲线(linear|ease|ease-in|ease-out|ease-in-out|cubic-bezier(n,n,n,n))
         *transition-delay，定义过渡效果何时开始
         */
        transition: all .5s;
        -moz-transition: all .5s; /* Firefox 4 */
        -webkit-transition: all .5s; /* Safari 和 Chrome */
        -o-transition: all .5s; /* Opera */
        /*初始化时每张海报都居中显示，然后通过 JavaScript 设置除中间海报外的随机位置，会产生从中间发散的效果时候时候，
         *切换中间海报的时候，由于中间海报瞬间失去了 .photo_center 的样式，但仍然还有 .photo 的样式，再被设置随机样式后会有平滑的过渡效果
         */
        left: 50%;
        top: 50%;
        margin: -160px 0 0 -130px;
      }
      .photo .photo-wrap .side{
        position: absolute;
        width: 100%;
        height: 100%;
        background-color: #eee;
        top: 0;
        right: 0;
        padding: 20px;
        /*IE8+、Opera 以及 Chrome 支持 box-sizing 属性，Firefox 支持替代的 -moz-box-sizing 属性。
         *box-sizing: content-box | border-box | inherit
         *content-box：默认值，按W3C盒模型进行处理 (element width = border + padding + border + content)
         *border-box：按IE6盒模型进行处理 (element width = content)
         */
        box-sizing: border-box;
        -moz-box-sizing：border-box;
        /*
         *IE 10+ 和 Firefox 支持 backface-visibility 属性，Opera 15+、Safari 和 Chrome 支持替代的 -webkit-backface-visibility 属性
         *backface-visibility 属性定义当元素不面向屏幕时是否可见
         *backface-visibility: visible | hidden
         *visible：背面是可见的
         *hidden：背面是不可见的
         */
        backface-visibility:hidden;
        -webkit-backface-visibility:hidden;	/* Chrome 和 Safari */
        -moz-backface-visibility:hidden;	 /* Firefox */
        -ms-backface-visibility:hidden;	 /* Internet Explorer */
      }
      .photo .photo-wrap .side-front{
        /*
         *IE 10、Firefox、Opera 支持 transform 属性，IE 9 支持替代的 -ms-transform 属性（仅适用于 2D 转换）
         *Safari 和 Chrome 支持替代的 -webkit-transform 属性（3D 和 2D 转换），Opera 只支持 2D 转换
         *transform: none | rotate | scale | skew | translate | matrix
         *如果有多个变换函数的时候，以空格分开
         *none: 表示不进行变换
         *rotate：旋转。rotate(<angle>) ：通过指定的角度参数对原元素指定一个 2D rotation，需先有 transform-origin 属性的定义
         *scale：缩放。元素的缩放中心点是元素的中心位置，缩放基数为1，如果其值大于1元素就放大，反之其值小于1，元素缩小；
         *scale(x,y)使元素水平方向和垂直方向同时缩放；如果第二个参数未提供，则取与第一个参数一样的值；
         *scaleX(x)元素仅水平方向缩放；scaleY(y)元素仅垂直方向缩放；其中 x, y 为数字
         *skew：扭曲。默认以元素中心为基点，也可以通过transform-origin来改变元素的基点位置；
         *skew(x,y)使元素在水平和垂直方向同时扭曲，如果没有设置第二个参数，那么Y轴为0deg；
         *skewX(x)仅使元素在水平方向扭曲变形；skewY(y)仅使元素在垂直方向扭曲变形，其中 x, y 为角度
         *translate：移动。移动物体时基点默认为元素中心点，也可以根据 transform-origin 进行改变基点；
         *translate(x,y)水平方向和垂直方向同时移动，如果第二个参数未提供，则以 0 作为其值；
         *translateX(x)仅水平方向移动；translateY(y)仅垂直方向移动；其中 x, y 为像素值
         *matrix：矩阵。
        
        transform: rotateY(0deg);
        -webkit-transform: rotateY(0deg);
        -moz-transform: rotateY(0deg);
        -o-transform: rotateY(0deg);
        -ms-transform: rotateY(0deg);
      }
      .photo .photo-wrap .side-front .image{
        width: 100%;
        height: 250px;
        line-height: 250px;
        overflow: hidden;
      }
      .photo .photo-wrap .side-front .image img{
        width: 100%;
        vertical-align: middle;/*使高度不够的图片居中显示*/
      }
      .photo .photo-wrap .side-front .caption{
        text-align: center;
        font-size: 16px;
        line-height: 50px;
      }
      /* 初始化时使 side-back 沿Y轴旋转180度，即在背面显示 */
      .photo .photo-wrap .side-back{
        transform: rotateY(180deg);
        -webkit-transform: rotateY(180deg);
        -moz-transform: rotateY(180deg);
        -o-transform: rotateY(180deg);
        -ms-transform: rotateY(180deg);
      }
      .photo .photo-wrap .side-back .desc{
        color: #666;
        font-size: 14px;
        line-height: 1.5em;
      }
      /*当前选中的海报样式*/
      .photo_center{
        top: 50%;
        left: 50%;
        margin: -160px 0 0 -130px;
        z-index: 2;
      }
      /*负责翻转*/
      .photo .photo-wrap{
        position: absolute;
        width: 100%;
        height: 100%;
        /*
         *IE不支持，Firefox 支持 transform-style 属性，Chrome、Safari 和 Opera 支持替代的 -webkit-transform-style 属性
         *transform-style 属性规定如何在 3D 空间中呈现被嵌套的元素。
         *transform-style: flat | preserve-3d
         *flat: 子元素将不保留其 3D 位置
         *preserve-3d: 子元素将保留其 3D 位置
         *一般而言，该声明应用在3D变换的兄弟元素们的父元素上，也就是舞台元素
        
        transform-style: preserve-3d;
        -webkit-transform-style: preserve-3d;
        transition: all .6s ease-in-out;
        -webkit-transition: all .6s ease-in-out;/* Safari 和 Chrome */
        -moz-transition: all .5s; /* Firefox 4 */
        -o-transition: all .5s; /* Opera */
        /*
         *IE 10、Firefox、Opera 支持 transform-origin 属性，IE 9 支持替代的 -ms-transform-origin 属性（仅适用于 2D 转换），
         *Safari 和 Chrome 支持替代的 -webkit-transform-origin 属性（3D 和 2D 转换），Opera 只支持 2D 转换
         *transform-origin: x-axis y-axis z-axis; 设置旋转元素的基点位置
         *x-axis：定义视图被置于 X 轴的何处。可能的值：left | center | right | length | %
         *y-axis：定义视图被置于 Y 轴的何处。可能的值：top | center | bottom | length | %
         *z-axis：定义视图被置于 Z 轴的何处。可能的值：length
         */
        transform-origin: 0% 50% 0px;
        -ms-transform-origin: 0% 50% 0px;/* IE 9 */
        -o-transform-origin: 0% 50% 0px;/* Opera */
        -webkit-transform-origin: 0% 50% 0px;/* Safari 和 Chrome */
        -moz-transform-origin: 0% 50% 0px;/* Firefox */
      }
      .photo_front .photo-wrap{/* .photo_front 是添加到 div.photo 的类 */
        transform: translate(0px, 0px) rotateY(0deg);
        -webkit-transform: translate(0px, 0px) rotateY(0deg);
        -moz-transform: translate(0px, 0px) rotateY(0deg);
        -o-transform: translate(0px, 0px) rotateY(0deg);
        -ms-transform: translate(0px, 0px) rotateY(0deg);
      }
      .photo_back .photo-wrap{/* .photo_back 是添加到 div.photo 的类*/
        transform: translate(260px, 0px) rotateY(180deg);
        -webkit-transform: translate(260px, 0px) rotateY(180deg);
        -moz-transform: translate(260px, 0px) rotateY(180deg);
        -o-transform: translate(260px, 0px) rotateY(180deg);
        -ms-transform: translate(260px, 0px) rotateY(180deg);
      }
      /* 添加控制按钮的样式 */
      @font-face{
        font-family: 'icomoon';
        src: url('images/icomoon.woff') format('woff');
        font-weight: normal;
        font-size: normal;
      }
      .nav{
        position: absolute;
        width: 80%;
        left: 10%;
        height: 30px;
        line-height: 30px;
        bottom: 20px;
        z-index: 3;
        text-align: center;
      }
      /* 普通样式 */
      .nav .i{
        display: inline-block;
        width: 30px;
        height: 30px;
        cursor: pointer;
        background-color: #aaa;
        text-align: center;
        border-radius: 50px;
        transform: scale(.5);
        -webkit-transform: scale(.5);
        -moz-transform: scale(.5);
        -o-transform: scale(.5);
        -ms-transform: scale(.5);
        transition: all .5s;
        -webkit-transition: all .5s;
        -moz-transition: all .5s;
        -o-transition: all .5s;
      }
      /* 设置并显示字体图标 */
      .nav .i:after{
        content: '\e965';
        font-family: 'icomoon';
        font-size: 80%;
        display: inline-block;
        line-height: 30px;
        text-align: center;
        color: #fff;
        opacity: 0;
      }
      /* 选中样式 */
      .nav .i_current{
        transform: scale(1);
        -webkit-transform: scale(1);
        -moz-transform: scale(1);
        -o-transform: scale(1);
        -ms-transform: scale(1);
      }
      .nav .i_current:after{
        opacity: 1;
      }
      /* 背面样式 */
      .nav .i_back{
        background-color: #555;
        transform: rotateY(180deg);
        -webkit-transform: rotateY(180deg);
        -moz-transform: rotateY(180deg);
        -o-transform: rotateY(180deg);
        -ms-transform: rotateY(180deg);
      }		
      .image img{
          width:200px;
          height:300px;
      }	
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap" id="wrap"></div>
    <script type="text/javascript" src="../JS/test.js"></script>
    <script type="text/javascript">
        // 3.通用函数，返回被选择的元素或元素集合
        function g(selector) {
            return selector.substring(0, 1) == '.' ? document.getElementsByClassName(selector.substring(1)) : document.getElementById(selector.substring(1));
        }
        /*随机数生成函数，在给定的范围内(random([min, max]))随机生成一个值，
         *因为此案例要在 20 张海报中随机选取一张居中显示，以及在左右两个分区内随机摆放
         *剩余海报，因为海报数量和左右区域都是有限制的，所以必须在给定范围内生成随机数。
         *传入的参数 range 是一个包含两个数值的数组，这两个数值即是给定范围的最小值(包含)和最大值(包含)
         *Math.random() 随机生成介于 0.0 ~ 1.0 之间的一个伪随机数
         */
        function random(range) {
            var min = Math.min(range[0], range[1]);
            var max = Math.max(range[0], range[1]);
            var diff = max - min;
            /*
             *例如 [1, 20]，则 diff = 19 --> 0 <= Math.round(Math.random() * diff) <= 19
             *然后再加上最小值，即可随机生成 1 ~ 20 之间的任意数，如果使用 Math.floor() 则
             *生成 1 ~ 19 之间的任意数，使用 Math.ceil() 则生成 2 ~ 20 之间的任意数
             */
            var number = Math.round(Math.random() * diff) + min;
            return number;
        }
        // 4.输出所有的海报
        /*
         *也可以使用模板替换的方法：
          <div class="wrap" id="wrap">
            <div class="photo  photo_front" onclick="turn(this)" id="photo_{{index}}"> <!-- div.photo负责位移、旋转（平面上xy旋转）-->
              <div class="photo-wrap"> <!-- div.photo-wrap负责3D翻转（正反面切换）-->
                <div class="side side-front">
                  <p class="image"><img src="images/{{img}}"></p>
                  <p class="caption">{{caption}}</p>
                </div>
                <div class="side side-back">
                  <p class="desc">{{desc}}</p>
                </div>
              </div>
            </div>
            <div class="nav">
              <span id="nav_{{index}}" onclick="turn(g('#photo_{{index}}'))" class="i"></span>
            </div>
          </div>
         *以上是静态模板，然后在 JavaScript 中对其中的 {{}} 关键字进行替换：
          function addPhotos(){
            var template = g('#wrap').innerHTML;
            var html = [];
            var nav=[];
            for(var i = 0; i < data.length; i++){//for in循环中的循环计数器是字符串，而不是数字它包含当前属性的名称或当前数组元素的索引
              var _html = template.replace('{{index}}', i + 1).replace('{{img}}', data[i].img).replace('{{caption}}', data[i].caption).replace('{{desc}}', data[i].desc);
              html.push(_html);
              nav.push('<span id="nav_' + (i + 1) + '" onclick="turn(g(\'#photo_' + (i + 1) + '\'))" class="i"></span>');
            }
            html.push('<div class="nav">' + nav.join('') + '</div>');
            g('#wrap').innerHTML = html.join('');
            rsort(random([1, data.length]));
          }
         *这样也可以实现输出所有海报，但是在刷新页面的一瞬间，文字部分会显示 {{}} 关键字没有被替换之前的样子，
         *因此我修改为纯 JavaScript 写入的方式
         */
        function addPhotos() {
            var _wrap = '';
            var _nav = '';
            for (var i = 0; i < data.length; i++) {//for in 循环中的循环计数器是字符串，而不是数字它包含当前属性的名称或当前数组元素的索引
                _wrap += '<div class="photo  photo_front" onclick="turn(this)" id="photo_' + (i + 1) + '"><div class="photo-wrap"><div class="side side-front"><p class="image"><img src="../Image/Gallery/' + data[i].img + '"></p><p class="caption">' + data[i].caption + '</p></div><div class="side side-back"><p class="desc">' + data[i].desc + '</p></div></div></div></div>';
                _nav += '<span id="nav_' + (i + 1) + '" onclick="turn(g(\'#photo_' + (i + 1) + '\'))" class="i"></span>';
            }
            var navigation = '<div class="nav">' + _nav + '</div>'
            g('#wrap').innerHTML = _wrap + navigation;
            rsort(random([1, data.length]));
        }
        addPhotos();
        // 6.计算左右分区的范围，使其他海报的显示不会超出此范围
        function range() {
            /*{left: {x: [左侧区域 left 的最小值, 左侧区域 left 的最大值], y: [左侧区域 top 的最小值, 左侧区域 top 的最大值]}, 
             *right: {x: [右侧区域 left 的最小值, 右侧区域 left 的最大值], y: [右侧区域 top 的最小值, 右侧区域 top 的最大值]}}
             */
            var range = {
                left: {
                    x: [],
                    y: []
                },
                right: {
                    x: [],
                    y: []
                }
            };
            //获取最外围容器的宽度和高度
            var wrap = {
                width: g('#wrap').clientWidth,
                height: g('#wrap').clientHeight
            };
            //获取每一张海报的宽度和高度，因为海报的大小都是一样的，所以取第一张
            var photo = {
                width: g('.photo')[0].clientWidth,
                height: g('.photo')[0].clientHeight
            };
            //按照自己想要显示的区域进行计算，左右区域的高度 (top) 范围是一样的
            range.left.x = [0 - photo.width / 4 + 130, wrap.width / 2 - photo.width * 5 / 4 + 130];
            range.left.y = [0 - photo.height / 4 + 160, wrap.height - photo.height * 3 / 4 + 160];
            range.right.x = [wrap.width / 2 + photo.width / 4 + 130, wrap.width - photo.width / 4 + 130];
            range.right.y = range.left.y;
            return range;
        }
        // 5.排序海报
        function rsort(n) {
            var _photo = g('.photo');
            var photos = [];
            for (var i = 0; i < _photo.length; i++) {
                _photo[i].className = 'photo photo_front';
                /*重排序之前去除所有图片样式*/
                _photo[i].style.left = '';
                _photo[i].style.top = '';
                /*一般来说，访问对象属性时使用的都是点表示法，不过，在 JavaScript 中也可以使用方括号表示法来访问
                 *对象的属性。在使用方括号语法时，应该将要访问的属性以字符串的形式放在方括号中，如下面的例子所示:
                  alert(person["name"]); //"Nicholas"
                  alert(person.name); //"Nicholas"
                 *方括号语法的主要优点是可以通过变量来访问属性，如果属性名中包含会导致语法错误的字符，或者属性名
                 *使用的是关键字或保留字，也可以使用方括号表示法。例如：
                  person["first name"] = "Nicholas";
                 *通常，除非必须使用变量来访问属性，否则建议使用点表示法。(《JavaScript 高级程序设计》P85)
                 */
                _photo[i].style['transform'] = _photo[i].style['-webkit-transform'] = 'scale(1.3)';
                photos.push(_photo[i]);
            }
            var photo_center = g('#photo_' + n);
            photo_center.className += ' photo_center';
            photo_center = photos.splice(n - 1, 1);//把photo_center从数组里删掉，splice() 方法会改变原始数组
            // 把剩下的海报分为左右两部分
            var photos_left = photos.splice(0, Math.ceil(photos.length / 2));
            var photos_right = photos;
            var ranges = range();
            // 对左右区域的海报位置进行随机赋值
            for (var j = 0; j < photos_left.length; j++) {
                var photo = photos_left[j];
                photo.style.left = random(ranges.left.x) + 'px';
                photo.style.top = random(ranges.left.y) + 'px';
                photo.style['transform'] = photo.style['-webkit-transform'] = 'rotate(' + random([-150, 150]) + 'deg) scale(1)';
            }
            for (var s = 0; s < photos_right.length; s++) {
                var photo = photos_right[s];
                photo.style.left = random(ranges.right.x) + 'px';
                photo.style.top = random(ranges.right.y) + 'px';
                photo.style['transform'] = photo.style['-webkit-transform'] = 'rotate(' + random([-150, 150]) + 'deg) scale(1)';
            }
            // 控制按钮处理
            var navs = g('.i');
            for (var k = 0; k < navs.length; k++) {
                navs[k].className = 'i';
            }
            g('#nav_' + n).className += ' i_current';
        }
        /*1.翻面控制，每个元素的 onclick() 事件都绑定此函数，如果点击的元素不是中间的海报，则取得其
         * id 进行重新排序，使其在中间显示；如果点击的是中间的海报则让它翻面，同时控制按钮也要翻面
         */
        function turn(elem) {
            var cls = elem.className;
            var n = elem.id.split('_')[1];//var n = elem.id.substr(-1, 1)，但是不推荐 substr;
            if (!/photo_center/.test(cls)) {//判断当前点击的元素是不是 photo_center，不是的话不执行后面的翻转而进行海报排序
                return rsort(n);
            }
            if (/photo_front/.test(cls)) {
                cls = cls.replace(/photo_front/, 'photo_back');
                g('#nav_' + n).className += ' i_back';//同时处理控制按钮
            } else {
                cls = cls.replace(/photo_back/, 'photo_front');
                g('#nav_' + n).className = g('#nav_' + n).className.replace(/\s*i_back\s*/, ' ');//同时处理控制按钮
            }
            elem.className = cls;
        }
    </script>
    </form>
</body>
</html>
