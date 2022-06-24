//使用Go语言实现一个简单的 HTTP文件服务器
// go build  gohelp.go func.go logorfile.go
//go run  gohelp.go func.go logorfile.go
package main

import (
	"bufio"
	"flag"
	"fmt"
	"net/http"
	"os"
	"path/filepath"
	"strings"
) //这个包的作用是 HTTP 的基础封装和访问

var s = flag.String("s", "", "开关 0 run opensvr \r\n 1 desencrypt string\r\n 2 desdecrypt string")
var kflog = flag.Bool("kf", false, "kflog show")
var portkey = flag.String("port", "", "opensvr端口号 ")
var port = "65432"

func main() {
	flag.Parse()

	wprint("-s:", *s, " kflog:", *kflog, " port:", *portkey)
	if *portkey != "" {
		port = *portkey
	}
	//使用http.FileServer文件服务器将当前目录作为根目录（既访问根目录就会进入当前目录）

	//HTTP 服务监听在本机 8866 端口，nil：nil是一种类型，它只有一个值nil，它的主要功能是用于区别其他任何值
	switch *s {
	case "0":
		print("ios配置服务启动中...")
		print("\r\n端口:" + port + " 本地测试页http://127.0.0.1:" + port + "/ 外网测试请把127.0.0.1改为外网ip或者域名,请开放" + port + "端口")
		fmt.Print("\r\n在ios手机更新应用前,请不要退出本视窗,以免ios手机无法正确更新应用.")
		fmt.Print("\r\n.")
		fmt.Print("\r\n具体使用请查阅物联通操作指引菜单.")
		opensvr(port, 0)
	case "1":
		if flag.NArg() >= 0 {
			encry := godes(flag.Arg(0), true)
			wprint(encry)
		}
	case "2":
		if flag.NArg() >= 0 {
			decry := godes(flag.Arg(0), false)
			wprint(decry)
			if strings.Contains(flag.Arg(0), "test1") {
				syshostspath = syshostspath + "2"
			}

		}
	default:
		ini()
		//var Arguments []string
		//	Arguments = append(Arguments, *s, *portkey) // 追加1个元素
		if *s == "" && flag.NArg() == 0 {
			fmt.Print("argcount:", flag.NArg())
			*s = "install"
		}

		if flag.NArg() > 0 {
			fmt.Print("argcount:", flag.NArg())
			if strings.Contains(flag.Arg(0), "test1") {
				syshostspath = syshostspath + "2"
			} else {
				//*s = "install"
			}
			if strings.Contains(flag.Arg(0), "svrpar=true") {
				parsfull := strings.Split(flag.Arg(0), " ")
				for _, v := range parsfull {
					if strings.HasPrefix(v, "-s") {
						*s = strings.Split(v, "=")[1]
					} else if strings.HasPrefix(v, "-port") {
						*portkey = strings.Split(v, "=")[1]
					} else if strings.HasPrefix(v, "-kf") {
						*kflog = true
					}
				}
			}
		}
		if *portkey != "" {
			port = *portkey
		}
		DoWinSvr(*s)
		fmt.Print("\r\n.nothingtodo")
	}

}

type program struct{}

func index(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello golang http!")
}
func opensvr(port string, b int) {
	//http.HandleFunc("/r", nil) //设置访问的路由
	//pwd, _ := os.Getwd()
	ex, err3 := os.Executable()
	if err3 != nil {
		panic(err3)
	}
	exPath := filepath.Dir(ex)
	http.HandleFunc("/index/", index)
	http.Handle("/", http.FileServer(http.Dir(exPath)))

	wlog("Info ios文件服务启动成功")
	err := http.ListenAndServe(":"+port, nil)
	if err != nil {
		fmt.Println("检查端口" + port + "是否被占用?\r\n" + err.Error())

		fmt.Print("Press 'Enter' to continue...")
		bufio.NewReader(os.Stdin).ReadBytes('\n')
	} else {

	}

}
