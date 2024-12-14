# Pacmen

## 关于项目部署到Saiblo
在Unity中，找到 file -> Build Settings -> Player Settings -> Publishing Settings 把 COmpression Format 改成 Disabled ，随后进行build.
build获得的文件夹里面有一个Build文件夹，打开里面的temp.loader.js,ctrl+F找到`document.URL`，改成 `https://player.dev.saiblo.net/Pacman/`。然后把飞书里的两个文件拷贝到根目录里。player.html是网页的入口，在其中修改至如下
```html
var buildUrl = "https://player.dev.saiblo.net/Pacman/Build";
      var loaderUrl = buildUrl + "/Pacman.loader.js";
      var config = {
        dataUrl: buildUrl + "/Pacman.data",
        frameworkUrl: buildUrl + "/Pacman.framework.js",
        codeUrl: buildUrl + "/Pacman.wasm",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "DefaultCompany",
        productName: "Pacman",
        productVersion: "0.1.0",
        matchWebGLToCanvasSize: true,
      };
```
index也类似（不知道有没有关系），然后更新到file.saiblo.dev.net上