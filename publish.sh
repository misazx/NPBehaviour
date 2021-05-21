npmServer="http://localhost:4873" ##声明值为 http:##192.168.6.59:4873 字符串的变量npmServer， 这个值是公司内部搭建的npm私有服务器的ip地址

loggedUser=$(npm whoami --registry $npmServer) ##将npm的当前登录的用户名赋值到loggedUser变量，npm whoami --registry $npmServer原意是将npmServer的当前用户名打印到控制台，$()这个bash语法将一些括号内执行的操作结果获得，然后用其外部的命令对这些结果进行操作，这里就是将输出的结果用等号赋值给loggedUser变量

if [ -z "$loggedUser" ];then ##if语句开始 意思是如果loggedUser这个变量是长度为0的字符串则为true
  npm adduser --registry $npmServer ## 创建或者验证当前npmServer所指地址的默认注册用户 
fi ##if语句结束
npm publish Assets/com.misa.npm --registry $npmServer
## 在npmServer指向的网址发布Assets/com.iyourcar.debugui路径指定的包