UnityAudioManager
===

UnityAudioManager is a Package that contains the following components:

* Audio Manager
* Event listener ( `play`, `pause` audio)

## Table of Contents

- [Requirement](#requirement)
- [UPM Package](#upm-package)
  - [Install via git URL](#install-via-git-url)

Requirement
---

- [Addressable](https://docs.unity3d.com/Packages/com.unity.addressables@1.21/manual/installation-guide.html)

UPM Package
---
### Install via git URL

Requires a version of unity that supports path query parameter for git packages (Unity >= 2022.3.0f1). You can add `https://github.com/Long18/UnityAudioManager.git?path=src/UnityAudioManager/Assets/Plugins/UnityAudioManager` to Package Manager

![image](https://github.com/Long18/UnityAudioManager/assets/28853225/6d95f1b0-1540-4fc1-b345-ee01f8576850)

![image](https://github.com/Long18/UnityAudioManager/assets/28853225/3a7fea8f-a8fd-4c14-9c6e-6bd24fc7215a)

or add `"com.long18.extensions-core": "https://github.com/Long18/UnityAudioManager.git?path=src/UnityAudioManager/Assets/Plugins/UnityAudioManager"` to `Packages/manifest.json`.

If you want to set a target version, UniTask uses the `*.*.*` release tag so you can specify a version like `#0.0.1`. For example `https://github.com/Long18/UnityAudioManager.git?path=src/UnityAudioManager/Assets/Plugins/UnityAudioManager#0.0.1`.
1