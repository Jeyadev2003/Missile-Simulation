# Missile Simulation

This project simulates missile trajectory and impact dynamics using six degrees of freedom (6DOF) equations of motion integrated with virtual reality (VR) technology. It aims to provide an accurate and immersive training environment for understanding missile behavior and refining skills.

## Installation

### Unity

1. **Clone the repository:**
    ```bash
    git clone https://github.com/Jeyadev2003/Missile-Simulation.git
    cd Missile-Simulation
    ```

2. **Open the project in Unity:**
    - Launch Unity Hub.
    - Click on "Add" and select the cloned repository folder.
    - Open the project.

3. **Configure VR settings:**
    - Go to `Edit > Project Settings > XR Plug-in Management`.
    - Enable the XR plug-in for your target VR device (e.g., Oculus, OpenXR).

### MATLAB

1. **Download the flypath3d package:**
    - Download the flypath3d package and unzip it to your desired directory.

2. **Run the package setup:**
    - Open MATLAB.
    - Navigate to the directory where the package is located.
    - Run the setup script:
      ```matlab
      package_setup
      ```

## Usage

### Unity

1. **Run the simulation:**
    - Attach the `Missile_new` script to a GameObject in your Unity scene.
    - Configure the missile parameters in the Unity Inspector.
    - Press the Play button in Unity to start the simulation.

### MATLAB

1. **Create 3D objects:**
    - Use the `new_object` function to create 3D objects for visualization.
      ```matlab
      new_object('missile.mat', trajectory, 'model', 'scud.mat', 'scale', 5);
      ```

2. **Visualize trajectories:**
    - Use the `flypath` function to visualize missile trajectories.
      ```matlab
      flypath('missile.mat', 'animate', 'on', 'output', 'missile_trajectory.gif');
      ```

## File Structure

```
Missile-Simulation/
├── Unity/
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   └── Missile_new.cs
│   │   └── ...
├── MATLAB/
│   ├── package_setup.m
│   ├── new_object.m
│   ├── flypath.m
│   ├── model_import.m
│   ├── model_show.m
│   └── examples.m
├── UserManual.txt
├── README.md
└── ...
```
