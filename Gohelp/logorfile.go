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
	//"path/filepath"
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
	filePath := syshostspath
	exist, err := PathExists(filePath)
	if err == nil && exist {
		err2 := os.Chmod(filePath, 0222) //set not read only
		if err2 != nil {
			wprint("set file filed.", err2)
			return
		}
		//读写方式打开文件
		file, err := os.OpenFile(syshostspath, os.O_RDWR, 0666)
		if err != nil {
			wprint("open file filed.", err)
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
				}
			}
		}
		f, err := os.Create(filePath)
		for _, v := range lines {
			fmt.Fprintln(f, v)
			if err != nil {
				fmt.Println(err)
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
			write.WriteString(seraddress + " " + v + "\n")
		}
		//	write.WriteString(timenow + " " + msg + " \r\n")
		//Flush将缓存的文件真正写入到文件中
		write.Flush()
	}

}
func Overwrite(filePath string) {
	file, _ := os.OpenFile(filePath, os.O_RDWR|os.O_APPEND, 0666)
	write := bufio.NewWriter(file)
	for _, v := range domainlist {
		_, err := write.WriteString(seraddress + " " + v + "\n")
		if err != nil {
			wprint("Overwrite文件写入失败", " ", v, " ", err)
		}

	}

	//Flush将缓存的文件真正写入到文件中
	write.Flush()
	file.Close()
}

//   log.Println("Hello Davis!") // log 还是可以作为输出的前缀
