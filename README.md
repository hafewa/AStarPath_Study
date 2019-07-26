# AStarPath_Study
基于Unity3D的A*寻路算法学习

【[Unity] A-Star(A星)寻路算法】https://www.cnblogs.com/yangyxd/articles/5447889.html

【A*算法】https://www.cnblogs.com/21207-iHome/p/6048969.html  
　　A*算法总结：  
　　1. 把起点加入 open list 。  
　　2. 重复如下过程：  
　　　　a. 遍历open list ，查找F值最小的节点，把它作为当前要处理的节点，然后移到close list中  
　　　　b. 对当前方格的 8 个相邻方格一一进行检查，如果它是不可抵达的或者它在close list中，忽略它。  
　　　　　否则，做如下操作：  
　　　　□  如果它不在open list中，把它加入open list，并且把当前方格设置为它的父亲  
　　　　□  如果它已经在open list中，检查这条路径 ( 即经由当前方格到达它那里 ) 是否更近。如果更近，  
　　　　　把它的父亲设置为当前方格，并重新计算它的G和F值。如果你的open list是按F值排序的话，  
　　　　　改变后你可能需要重新排序。  
　　　　c. 遇到下面情况停止搜索：  
　　　　□  把终点加入到了 open list 中，此时路径已经找到了，或者  
　　　　□  查找终点失败，并且open list 是空的，此时没有路径。  
　　3. 从终点开始，每个方格沿着父节点移动直至起点，形成路径。