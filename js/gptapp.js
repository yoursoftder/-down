 
document.write("<script type='text/javascript' async src='https://code.jquery.com/jquery-3.7.0.js'><\/script>");
document.write("<script type='text/javascript' async src='https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.8.0/highlight.min.js'><\/script>");
loadjs('https://cdn.jsdelivr.net/npm/marked/marked.min.js');
function loadjs(jsurl){
	var script = document.createElement('script');
script.type = 'text/javascript';
script.src = jsurl;
document.head.appendChild(script);
}
var ondebug = false;
var defchaturl="https://sub.rm178.cn";
var defchaturlendpoint="/api/MainProduct/chat";
if(!ondebug){
	domainInput.style.display = 'none';
}
const ua = navigator.userAgent;
const iOS = /iPad|iPhone|iPod/.test(ua);
 function isMobile() {
  return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
}
  
var inputBox = document.getElementById("line");
var toptip = document.getElementById("toptip");
inputBox.addEventListener("click", function() {
 


});


  
// 获取拖动元素
var dragElement = document.getElementById("inputout");
var dragElementico = document.getElementById("inputspeck");

// 记录鼠标按下时的初始位置
var initialX;
var initialY;

// 记录拖动元素的初始位置
var offsetX = 0;
var offsetY = 0;

// 鼠标按下时的事件处理函数
function F5() {
	// var Dt = new Date();var randomstr = ''+Dt.getMonth() + Dt.getDay() + Dt.getHours();
	  location.replace(window.location.origin+window.location.pathname+'?'+Math.random());
}
function afterhtmlload() {
	 if (isMobile) {
    var inputOut =dragElement;
    inputOut.style.top = "10%";
    toptip.style.marginTop  = "2%";
	
  }
   

  marked.setOptions({
    renderer: new marked.Renderer(),
    gfm: true,
    tables: true,
    breaks: false,
    pedantic: false,
    sanitize: false,
    smartLists: true,
    smartypants: false
   
});
}
function dragStart(event) {
  initialX = event.clientX;
  initialY = event.clientY;

  // 获取拖动元素的当前位置
  var rect = dragElementico.getBoundingClientRect();
  offsetX = initialX - rect.left;
  offsetY = initialY - rect.top;

  // 添加鼠标移动和鼠标释放事件监听器
  document.addEventListener("mousemove", drag);
  document.addEventListener("mouseup", dragEnd);
}

// 鼠标移动时的事件处理函数
function drag(event) {
  // 计算拖动元素的新位置
  var newX = event.clientX - offsetX;
  var newY = event.clientY - offsetY;

  // 更新拖动元素的位置
  dragElement.style.left = newX + "px";
  dragElement.style.top = newY + "px";
}

// 鼠标释放时的事件处理函数
function dragEnd(event) {
  // 移除鼠标移动和鼠标释放事件监听器
  document.removeEventListener("mousemove", drag);
  document.removeEventListener("mouseup", dragEnd);
}

// 添加鼠标按下事件监听器
dragElementico.addEventListener("mousedown", dragStart);


window.addEventListener("keydown", (e) => {
  if (e.key === "Escape") {
    showSettings(false)
    showHistory(false)
  }
  if ((e.ctrlKey || e.altKey)) {
    // console.log(e.key);
    switch (e.key) {
      case "i":
        e.preventDefault()
        reset()
        break;
      case ",":
        e.preventDefault()
        showSettings(true)
        break;
      case "h":
        e.preventDefault()
        showHistory(true)
        break;
      case ";":
        e.preventDefault()
        config.multi = !config.multi
        addItem("system", "Long conversation checked: " + config.multi)
        break;
      case "b":
        e.preventDefault()
        speechToText()
        break;

      default:
        break;
    }
  }
}, { passive: false })

line.addEventListener("keydown", (e) => {
  if (e.key == "Enter" && (e.ctrlKey || e.altKey)) {
    e.preventDefault()
    onSend()
  }
})

line.addEventListener("paste", (e) => {
  e.preventDefault()

  let clipboardData = (e.clipboardData || window.clipboardData)
  let paste = clipboardData.getData("text/plain")
    .toString()
    .replaceAll("\r\n", "\n")
	  let line = document.querySelector("#line");
  line.focus()
  document.execCommand("insertText", false, paste)
}, { passive: false })

function resend() {
const box = document.getElementById("box");
const userElements = box.getElementsByClassName("user");
const lastUserElement = userElements[userElements.length - 1];
const content = lastUserElement.textContent;
 document.getElementById("line").innerText=content;
//onSend();
//console.log(content); // 输出: "最后一个user"
}

function onSend() {
  //var value = (line.value || line.innerText).trim()//org
  var line = document.getElementById("line");

  var value = (line.value || line.innerText).trim()

  if (!value) return

  addItem("user", value)
  postLine(value)

  line.value = ""
  line.innerText = ""
}

function addItem(type, content) {
  let request = document.createElement("div")  
  let line = document.querySelector("#line");
  var box = document.getElementById("box");
  request.className = type
  //判断box儿子数
 
var childDivs = box.querySelectorAll("div");
var count = childDivs.length;
var fixtype = type;
if(fixtype==''||isNaN(fixtype)) fixtype='assistant';
  request.setAttribute("id", ''+fixtype+count);//flag id
   if(typeof marked === 'undefined'){
    setTimeout(null, 100); // 延迟100毫秒后再次尝试执行a1()函数
    return;
  }
 marked.setOptions({
    highlight: function (code) {    //这个code后还有2个参数lang,callback
    return hljs.highlightAuto(code).value;
    }
});
if(ondebug)
	console.log('type:'+type+" content:"+content);
if(type=='assistant'||type ==''||isNaN(type)){
  request.innerHTML  = marked.parse(content)
}else{
  request.innerText  = content;
}
  box.appendChild(request)

  window.scrollTo({
    top: document.body.scrollHeight, behavior: "auto",
  })

 
box.scrollTop = box.scrollHeight; // 设置滚动条位置为内容高度
  line.focus()

  return request
}
function showdomain(){
	if(apiKeyInput.value=='327')
		domainInput.style.display = '';
}
function postLine(line) {
  saveConv({ role: "user", content: line })
  let reqMsgs = []
  if (messages.length < 10) {
    reqMsgs.push(...messages)
  } else {
    reqMsgs.push(messages[0])
    reqMsgs.push(...messages.slice(messages.length - 4, messages.length))
  }
  if (config.model === "gpt-3.5-turbo") {
    chat(reqMsgs)
  } else {
    completions(reqMsgs)
  }
}

var convId;
var messages = [];
function chat(reqMsgs) {
  let assistantElem = addItem('', '')
  let _message = reqMsgs
  if (!config.multi) {
    _message = [reqMsgs[0], reqMsgs[reqMsgs.length - 1]]
  }
 // if(config.apiKey=="")
	 // config.apiKey="sk-rrLxGDKNnlspRxEi1bThT3BlbkFJBtgXpyIx";
    if(config.domain==""||config.domain=="https://api.openai.com")
 	config.domain=defchaturl;
var apiurl ="/v1/chat/completions";
if(config.domain==defchaturl||config.domain.indexOf('918')!=-1)
	apiurl = defchaturlendpoint;

  send(`${config.domain}${apiurl}`, {
    "model": "gpt-3.5-turbo",
    "messages": _message,
    "max_tokens": config.maxTokens,
    "stream": config.stream,
    "temperature": config.temperature,
  }, (data) => {
    let msg = data.choices[0].delta || data.choices[0].message || {}
    assistantElem.className = 'assistant'
    assistantElem.innerText += msg.content || ""
  }, () => onSuccessed(assistantElem),)
}
function completions(reqMsgs) {
  let assistantElem = addItem('', '')
  let _prompt = ""
  if (config.multi) {
    reqMsgs.forEach(msg => {
      _prompt += `${msg.role}: ${msg.content}\n`
    });
  } else {
    _prompt += `${reqMsgs[0].role}: ${reqMsgs[0].content}\n`
    let lastMessage = reqMsgs[reqMsgs.length - 1]
    _prompt += `${lastMessage.role}: ${lastMessage.content}\n`
  }
  _prompt += "assistant: "
  send(`${config.domain}/v1/completions`, {
    "model": config.model,
    "prompt": _prompt,
    "max_tokens": config.maxTokens,
    "temperature": 0,
    "stop": ["\nuser: ", "\nassistant: "],
    "stream": config.stream,
    "temperature": config.temperature,
  }, (data) => {
    assistantElem.className = 'assistant'
    assistantElem.innerText += data.choices[0].text
  }, () => onSuccessed(assistantElem),)
}
function onSuccessed(assistantElem) {
  let msg = assistantElem.innerText
  saveConv({ role: "assistant", content: msg })
  if (config.tts) {
    textToSpeech(msg)
  }

  
}
function sendover(){
	var box = document.getElementById("box");
  //判断box儿子数 
var childDivs = box.querySelectorAll("div");
var count = childDivs.length-1;
var assistantdiv = document.getElementById("assistant"+count);
if(ondebug) console.log('childDivs.length:'+count); 
//assistantdiv.remove();
var orgtxt = assistantdiv.innerText;

assistantdiv.innerHTML = marked.parse(orgtxt);
//addItem('assistant',orgtxt);
}
 
function send(reqUrl, body, onMessage, scussionCall) {
  loader.hidden = false
  let onError = (data) => {
    console.error(data);
    loader.hidden = true
    if (!data) {
      addItem("system", `Unable to access OpenAI, please check your network.`)
    } else {
      try {
        let openai = JSON.parse(data)
        addItem("system", `${openai.error.message}`)
      } catch (error) {
        addItem("system", `${data}`)
      }
    }
  }
  if (!config.tts) {
    body.stream = true
    var source = new SSE(
      reqUrl, {
      headers: {
        "Authorization": "Bearer " + config.apiKey,
        "Content-Type": "application/json",
      },
      method: "POST",
      payload: JSON.stringify(body),
    });

    source.addEventListener("message", function (e) {
		if(ondebug)
		console.log(e.data);
      if (e.data == "[DONE]") {
        loader.hidden = true
        scussionCall()
		sendover();
      } else {
        try {
          onMessage(JSON.parse(e.data))
        } catch (error) {
          onError(error)
        }
      }
    });

    source.addEventListener("error", function (e) {
      onError(e.data)
    });

    source.stream();
  } else {
    body.stream = false
    fetch(reqUrl, {
      method: "POST",
      headers: {
        "Authorization": "Bearer " + config.apiKey,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    }).then((resp) => {
      return resp.json()
    }).then((data) => {
      loader.hidden = true
      if (data.error) {
        throw new Error(`${data.error.code}: ${data.error.message}`)
      }
      onMessage(data)
      scussionCall()
    }).catch(onError)
  }
}

function reset() {

  var box = document.getElementById("box");
var children = box.querySelectorAll("*");

for (var i = 0; i < children.length; i++) {
  children[i].remove();
}
  convId = uuidv4();
 
 if (config.hasOwnProperty('content'))  {
  addItem(config.firstPrompt.role, config.firstPrompt.content)
}else{
	if (!config.firstPrompt) {
  config.firstPrompt = { role: "system", content: systemPromptInput.placeholder };
}
 
  addItem(config.firstPrompt.role, config.firstPrompt.content)
} 
messages = [config.firstPrompt]
}
const convKey = "conversations_"
var cansave = true;
function isChecked(checkbox) {
   
  cansave=checkbox.checked;
}
function saveConv(message) {
  messages.push(message)
  if(cansave){
	  messages = messages.filter(function(message) {
    return message.role !== "system";
});
  localStorage.setItem(`${convKey}${convId}`, JSON.stringify(messages))
  	  document.getElementById("reset2").value = String.fromCharCode(0x24C8);
  }else{
	  document.getElementById("reset2").value = String.fromCharCode(0x2694);


  }
}

function switchConv(key) {
  if (key == null) {
    addItem("system", "No conversations")
    return
  }
  box.innerHTML = ''
  messages = JSON.parse(localStorage.getItem(key))
  messages.forEach(msg => {
    addItem(msg.role, msg.content)
  });
  convId = key.substring(convKey.length);
}

function deleteConv(key) {
  localStorage.removeItem(key)
}

function showHistory(ok = true) {
  if (ok) {
    historyModal.style.display = ''
    historyList.innerHTML = ''
    for (let index = 0; index < localStorage.length; index++) {
      let key = localStorage.key(index);
      if (key.substring(0, convKey.length) != convKey) { continue }
      let itemJson = localStorage.getItem(key)
      let itemData;
      try {
        itemData = JSON.parse(itemJson)
      } catch (error) {
        continue
      }
      historyList.innerHTML += `<div class="history-item">
        <div style="flex: 1;" onclick='switchConv("${key}"); showHistory(false);'>
          <div>SYST: ${itemData[0].content}</div>
          <div>USER: ${itemData[1].content} (${itemData.length}+)</div>
        </div>
        <button onclick='deleteConv("${key}"); showHistory(true);' class="icon" title="Delete">❌</button>
</div>`
    }
    if (0 == localStorage.length) {
      historyList.innerHTML = `<h4>There are no past conversations yet.</h4>`
    } else {
    }
  } else {
    historyModal.style.display = 'none'
  }
}

function showSettings(ok = true) {
  if (ok) {
    settingsModal.style.display = ''
    setSettingInput(config)
  } else {
    settingsModal.style.display = 'none'
  }
}

function setSettingInput(config) {
  domainInput.placeholder = "https://api.openai.com";
 
  maxTokensInput.placeholder = config.maxTokens
  systemPromptInput.placeholder = "有什么问题请详细描述告诉我,不要问在不在哦."
  temperatureInput.placeholder = config.temperature

  apiKeyInput.value = config.apiKey

  if (!config.domain) {
    config.domain = domainInput.placeholder
  } else {
    domainInput.value = config.domain
  }
  if (!config.maxTokens) {
    config.maxTokens = parseInt(maxTokensInput.placeholder)
  } else {
    maxTokensInput.value = config.maxTokens
  }
  if (!config.temperature) {
    config.temperature = parseInt(temperatureInput.placeholder)
  } else {
    temperatureInput.value = config.temperature
  }
  if (!config.model) {
    config.model = "gpt-3.5-turbo"
  }
  modelInput.value = config.model
  if (!config.firstPrompt) {
    config.firstPrompt = { role: "system", content: systemPromptInput.placeholder }
  } else {
    systemPromptInput.value = config.firstPrompt.content
  }
  multiConvInput.checked = config.multi
  ttsInput.checked = config.tts
  whisperInput.checked = config.onlyWhisper
}

var config = {
  domain: "",
  apiKey: "",
  maxTokens: 500,
  model: "gpt-3.5-turbo",
  firstPrompt: null,
  multi: true,
  stream: true,
  prompts: [],
  temperature: 0.3,
  tts: false,
  onlyWhisper: false,
}
function saveSettings() {
  if (!apiKeyInput.value) {
    alert('OpenAI API key can not empty')
    return
  }
  config.domain = domainInput.value || domainInput.placeholder
  config.apiKey = apiKeyInput.value
  config.maxTokens = parseInt(maxTokensInput.value || maxTokensInput.placeholder)
  config.temperature = parseInt(temperatureInput.value || temperatureInput.placeholder)
  config.model = modelInput.value
  if (systemPromptInput.value) {
    config.firstPrompt = {
      role: "system",
      content: (systemPromptInput.value || systemPromptInput.placeholder)
    }
  }
  messages[0] = config.firstPrompt
  config.multi = multiConvInput.checked
  config.tts = ttsInput.checked
  config.onlyWhisper = whisperInput.checked
  try{
  box.firstChild.innerHTML = config.firstPrompt.content
  }catch{}
  localStorage.setItem("conversation_config", JSON.stringify(config))
  showSettings(false)
  addItem('system', 'Update successed')
}

function onSelectPrompt(index) {
  let prompt = config.prompts[index]
  systemPromptInput.value = prompt.content
  multiConvInput.checked = prompt.multi
  promptDetails.open = false
}

function init() {
  let configJson = localStorage.getItem("conversation_config")
  let _config = JSON.parse(configJson)
  if (_config) {
    let ck = Object.keys(config)
    ck.forEach(key => {
      config[key] = _config[key] || config[key]
    });
    setSettingInput(config)
  } else {
	  if(ondebug)
    showSettings(true)
  }
  recogLangInput.value = navigator.language
  if (!('speechSynthesis' in window)) {
    ttsInput.disabled = false
    ttsInput.onclick = () => {
      alert("The current browser does not support text-to-speech");
    }
  }
if(ondebug)
  fetch("./js/gptprompts.json").then(resp => {
    if (!resp.ok) {
      throw new Error(resp.statusText)
    }
    return resp.json()
  }).then(data => {
    config.prompts = data
    for (let index = 0; index < data.length; index++) {
      const prompt = data[index];
      promptList.innerHTML += promptDiv(index, prompt)
    }
  })

  reset()
}

window.scrollTo(0, document.body.clientHeight)
init()

const promptDiv = (index, prompt) => {
  return `<div style="margin-top: 15px; cursor: pointer;" onclick="onSelectPrompt(${index})">
<div style="display: flex;">
  <strong style="flex: 1;">${prompt.title}</strong>
  <label style="display:  ${prompt.multi ? "" : "none"}; align-items: center; margin: 0">
    <span style="white-space: nowrap;">Long conversation</span>
    <input type="checkbox" style="width: 1.1rem; height: 1.1rem;" checked disabled/>
  </label>
</div>
<div style="margin-top: 2px;">${prompt.content}</div>
</div>`
}

const textToSpeech = async (text, options = {}) => {
  loader.hidden = false
  const synth = window.speechSynthesis;

  // Check if Web Speech API is available
  if (!('speechSynthesis' in window)) {
    loader.hidden = true
    alert("The current browser does not support text-to-speech");
    return;
  }

  // Detect language using franc library
  const { franc } = await import("https://cdn.jsdelivr.net/npm/franc@6.1.0/+esm");
  let lang = franc(text);
  if (lang === "" || lang === "und") {
    lang = navigator.language
  }
  if (lang === "cmn") {
    lang = "zh-CN"
  }

  // Get available voices and find the one that matches the detected language
  const voices = await new Promise(resolve => {
    const voices = synth.getVoices();
    resolve(voices);
  });
  const voice = voices.find(v => langEq(v.lang, lang) && !v.localService);
  if (!voice) {
    voice = voices.find(v => langEq(v.lang, navigator.language) && !v.localService);
  }

  // Create a new SpeechSynthesisUtterance object and set its parameters
  const utterance = new SpeechSynthesisUtterance(text);
  utterance.voice = voice;
  utterance.rate = options.rate || 1.0;
  utterance.pitch = options.pitch || 1.0;
  utterance.volume = options.volume || 1.0;

  // Speak the text
  synth.speak(utterance);
  utterance.addEventListener('boundary', (event) => {
    const { charIndex, elapsedTime } = event;
    const progress = charIndex / utterance.text.length;
    // console.log(`当前朗读进度：${progress * 100}%, 时间：${elapsedTime}`);
    loader.hidden = true
  });
};

const regionNamesInEnglish = new Intl.DisplayNames(['en'], { type: 'language' });
const langEq = (lang1, lang2) => {
  let langStr1 = regionNamesInEnglish.of(lang1)
  let langStr2 = regionNamesInEnglish.of(lang2)
  if (langStr1.indexOf(langStr2) !== -1) return true
  if (langStr2.indexOf(langStr1) !== -1) return true
  return langStr1 === langStr2
}

const getVoices = () => {
  return new Promise(resolve => {
    synth.onvoiceschanged = () => {
      const voices = synth.getVoices();
      resolve(voices);
    };
  });
}

var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition
// var SpeechGrammarList = SpeechGrammarList || window.webkitSpeechGrammarList
// var SpeechRecognitionEvent = SpeechRecognitionEvent || webkitSpeechRecognitionEvent
var recognition = null;
const _speechToText = () => {
  loader.hidden = false
  // const recognition = new (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition)();
  if (!recognition) {
    recognition = new SpeechRecognition();

    recognition.continuous = false;
    recognition.lang = recogLangInput.value;
    recognition.interimResults = false;
    recognition.maxAlternatives = 1;

    recognition.onresult = (event) => {
      loader.hidden = true
      try {
        const speechResult = event.results[0][0].transcript;
        line.innerText = speechResult;
        // onSend()
      } catch (error) {
        addItem('system', `Speech recogniion result failed: ${error.message}`)
      }
    };

    recognition.onspeechend = function () {
      loader.hidden = true
      recognition.stop();
    };

    recognition.onnomatch = function (event) {
      loader.hidden = true
      addItem('system', `Speech recogniion match failed: ${event.error}`)
    }

    recognition.onerror = (event) => {
      loader.hidden = true
      addItem('system', `Speech recogniion error: ${event.error}, ${event}`)
    };
  }

  try {
    recognition.start();
  } catch (error) {
    onError(`Speech error: ${error}`)
  }
}

function _speechToText1() {
	
  loader.hidden = false
  // 获取音频流
  navigator.mediaDevices.getUserMedia({ audio: true })
    .then(function (stream) {
      // 创建 MediaRecorder 对象
      const mediaRecorder = new MediaRecorder(stream);
      // 创建 AudioContext 对象
      const audioContext = new AudioContext();
      // 创建 MediaStreamAudioSourceNode 对象
      const source = audioContext.createMediaStreamSource(stream);
      // 创建 MediaStreamAudioDestinationNode 对象
      const destination = audioContext.createMediaStreamDestination();
      // 将 MediaStreamAudioDestinationNode 对象连接到 MediaStreamAudioSourceNode 对象
      source.connect(destination);
      // 将 MediaStreamAudioDestinationNode 对象的 MediaStream 传递给 MediaRecorder 对象
      mediaRecorder.stream = destination.stream;
      // 创建一个空的音频缓冲区
      let chunks = [];
      // 开始录音
      mediaRecorder.start();
      // 监听录音数据
      mediaRecorder.addEventListener('dataavailable', function (event) {
        chunks.push(event.data);
      });
      // 停止录音
      mediaRecorder.addEventListener('stop', function () {
        // 将录音数据合并为一个 Blob 对象
        const blob = new Blob(chunks, { type: 'audio/mp3' });
        // 创建一个 Audio 对象
        const audio = new Audio();
        // 将 Blob 对象转换为 URL
        const url = URL.createObjectURL(blob);
        // 设置 Audio 对象的 src 属性为 URL
        audio.src = url;
        // 播放录音
        audio.play();
        // asr
        transcriptions(getRecordFile(chunks, mediaRecorder.mimeType))
      });
      // 5 秒后停止录音
      setTimeout(function () {
        mediaRecorder.stop();
        stream.getTracks().forEach(track => track.stop());
      }, 5000);
    })
    .catch(function (error) {
      console.error(error);
    });
}

const transcriptions = (file) => {
  const formData = new FormData();
  formData.append("model", "whisper-1");
  formData.append("file", file);
  formData.append("response_format", "json");
  fetch("https://openai.icsq.xyz/v1/audio/transcriptions", {
    method: "POST",
    headers: {
      "Authorization": "Bearer " + config.apiKey,
    },
    body: formData,
  }).then((resp) => {
    return resp.json()
  }).then((data) => {
    loader.hidden = true
    if (data.error) {
      throw new Error(`${data.error.code}: ${data.error.message}`)
    }
    line.innerText = data.text
    line.focus()
  }).catch(e => {
    loader.hidden = true
    addItem("system", e)
  })
}

const getRecordFile = (chunks, mimeType) => {
  const dataType = mimeType.split(';')[0];
  const fileType = dataType.split('/')[1];
  const blob = new Blob(chunks, { type: dataType });
  const name = `input.${fileType}`
  return new File([blob], name, { type: dataType })
}

const speechToText = () => {
	return;
  loader.hidden = false
  // 获取音频流
  navigator.mediaDevices.getUserMedia({ audio: true })
    .then(function (stream) {
      // 创建 MediaRecorder 对象
      const mediaRecorder = new MediaRecorder(stream);
      // 创建 AudioContext 对象
      const audioContext = new AudioContext();
      // 创建 MediaStreamAudioSourceNode 对象
      const source = audioContext.createMediaStreamSource(stream);
      // 创建 MediaStreamAudioDestinationNode 对象
      const destination = audioContext.createMediaStreamDestination();
      // 将 MediaStreamAudioDestinationNode 对象连接到 MediaStreamAudioSourceNode 对象
      source.connect(destination);
      // 将 MediaStreamAudioDestinationNode 对象的 MediaStream 传递给 MediaRecorder 对象
      mediaRecorder.stream = destination.stream;
      // 创建一个空的音频缓冲区
      let chunks = [];
      // 开始录音
      mediaRecorder.start();
      // 监听录音数据
      mediaRecorder.addEventListener('dataavailable', function (event) {
        chunks.push(event.data);
      });
      // 停止录音
      mediaRecorder.addEventListener('stop', function () {
        console.log("stop record");
        const audiofile = getRecordFile(chunks, mediaRecorder.mimeType)
        // 将录音数据合并为一个 Blob 对象
        // const blob = new Blob(chunks, { type: 'audio/mp3' });
        // 创建一个 Audio 对象
        const audio = new Audio();
        // 将 Blob 对象转换为 URL
        const url = URL.createObjectURL(audiofile);
        // 设置 Audio 对象的 src 属性为 URL
        audio.src = url;
        // 播放录音
        audio.play();
        // 如果仅使用 Whisper 识别，则直接调用
        if (config.onlyWhisper) {
          transcriptions(audiofile)
        }
      });
      if (config.onlyWhisper) {
        detectStopRecording(stream, 0.38, () => {
          if (mediaRecorder.state === 'recording') {
            mediaRecorder.stop();
          }
          stream.getTracks().forEach(track => track.stop());
        })
      } else {
        asr(
          onstop = () => {
            addItem("system", `Stoped record: read ${chunks.length} "${mediaRecorder.mimeType}" blob, and start recognition`);
            if (mediaRecorder.state === 'recording') {
              mediaRecorder.stop();
            }
            stream.getTracks().forEach(track => track.stop());
          },
          onnomatch = () => {
            transcriptions(getRecordFile(chunks, mediaRecorder.mimeType))
          },
          onerror = () => {
            transcriptions(getRecordFile(chunks, mediaRecorder.mimeType))
          })
      }
    })
    .catch(function (error) {
      console.error(error);
      addItem("system", error);
    });
}

const asr = (onstop, onnomatch, onerror) => {
  const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition
  const recognition = new SpeechRecognition()

  recognition.continuous = false;
  recognition.lang = recogLangInput.value;
  recognition.interimResults = false;
  recognition.maxAlternatives = 1;

  recognition.onresult = (event) => {
    loader.hidden = true
    try {
      const speechResult = event.results[0][0].transcript;
      line.innerText = speechResult;
      // onSend()
    } catch (error) {
      addItem('system', `Speech recogniion result failed: ${error.message}`)
    }
  };

  recognition.onspeechend = function () {
    recognition.stop();
    onstop();
  };

  recognition.onnomatch = onnomatch

  recognition.onerror = onerror

  try {
    recognition.start();
  } catch (error) {
    onerror()
  }
}

function detectStopRecording(stream, maxThreshold, callback) {
  const audioContext = new AudioContext();
  const sourceNode = audioContext.createMediaStreamSource(stream);
  const analyzerNode = audioContext.createAnalyser();
  analyzerNode.fftSize = 2048;
  analyzerNode.smoothingTimeConstant = 0.8;
  sourceNode.connect(analyzerNode);
  const frequencyData = new Uint8Array(analyzerNode.frequencyBinCount);
  var startTime = null;
  const check = () => {
    analyzerNode.getByteFrequencyData(frequencyData);
    const amplitude = Math.max(...frequencyData) / 255;
    console.log(`amplitude: ${amplitude}`);
    if (amplitude >= maxThreshold) {
      console.log("speeching");
      startTime = new Date().getTime();
      requestAnimationFrame(check);
    } else if (startTime && (new Date().getTime() - startTime) > 1000) {
      callback('stop');
    } else {
      console.log("no speech");
      requestAnimationFrame(check);
    }
  };
  requestAnimationFrame(check);
}
