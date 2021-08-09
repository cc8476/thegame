UI元素，都必须在canvas上面




//创建ui
Rect aaa = new Rect(110, 110, 110, 110);
GUI.TextField(aaa, "dddd");




### GUI

坐标是左上角(0,0)
private void OnGUI() 不建议在项目中正式只有，一般用于创建调试面板


## 下面的控件都是ugui

### Image
Image控件主要是用来显示图片，显示图片的格式是Sprite
image的image type，有一个sliced,表示九宫格分割；
前提是需要对sprite做处理，设置sprite editor,设置4个边界

image的image type，有一个tiled,表示平铺，用来做背景

image的image type，有一个filled ,用来做技能冷却


# RawImage 
通过设置 uvRect，来实现图象自身的位移或者scale变化
或者形成数字图片，例如图片是0123456789  ，设置不同的uvRect.x 来切换数字

# Button
通过设置Transition
1.sprite swap :不同的图片，来表示不同的button状态
2.animation  :不同的动画...

绑定事件，并带参数
string param= "asssss";
playBtn.GetComponent<Button>().onClick.AddListener( ()=>playFunc(param)    );

# scrollview
可以做背包

# rect transform
Pivot:轴点，ui对象旋转的中心点 (默认是0.5,0.5) 
anchors:锚点， 固定位置，在父容器中的位置！选点锚点后，父容器不管如何变化，子容器到父容器的相对位置就会固定下来了

anchors —— 锚点:相对位置固定，不会变形
anchors —— 锚线/锚框:相对位置固定，可能会变形

# canvas 
渲染模式
1.overlay  表示永远在屏幕前面

scaler:
1.constant pixel :ui尺寸恒定，和分辨率无关
2.sacle with screen size ,和分辨率等比缩放，会提供一个参考分辨率



canvas 使用world space的 render type是否更好？因为可以设置canvas自身的position
其余2个都不可设置；




Unity GUI 之定位RaycastTarget取消不必要交互 ：类似于去掉鼠标监听，这样就不会阻挡其他元素触发事件了
