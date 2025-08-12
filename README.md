# Unity Playmode Preserver

**Unity Playmode Preserver** is a Unity Editor tool that allows you to **preserve and apply changes** made to GameObject transforms during Play Mode. This is particularly useful for level design, prototyping, and fine-tuning scene setups without losing tweaks made during runtime.

---

## Features

- Track and compare pre- and post-Play Mode `Transform` data.
- Visual editor window for reviewing changed GameObjects.
- Apply transform changes made in Play Mode directly to your scene.
- Easily extensible to support other components like `Rigidbody`, `Collider`, etc.
- Designed as a reusable Unity package.

---

## How to Use

1. Select gameobjects you want to track transform of.

2. Make your changes during Play Mode.

3. After exiting Play Mode, you will get a window informing you of changes detected. Apply one by one.

---

## Installation

### Option 1: Manual

1. Download or clone this repository.
2. Copy the entire folder into your Unity project's `Assets/Plugins` or `Packages` folder.

### Option 2: Git URL (UPM)

Add the following to your project's `manifest.json`:

```json
"dependencies": {
  "com.aliameenrana.playmodepreserver": "https://github.com/aliameenrana/Unity-Playmode-Preserver.git"
}
