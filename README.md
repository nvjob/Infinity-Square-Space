# Infinity Square/Space.
### The prototype of the game is open source. Version 1.0

**Features: infinite procedurally generated world, almost complete destructible of everything, a very large number of NPCs (up to 1000 in one planetary system), battles involving hundreds of NPS, gravity is an important game element.**

This prototype of the game is completely playable, but nevertheless, this is not a complete game.
The source contains all the tools for the development of the game, but you need the above-average programming skill. There are no comments in the code, but the code itself is well structured, all the scripts and shaders, functions and variables are named so that it is clear what they are responsible for.

**This game prototype consists of five main parts:**
- Pools of static game objects.
- Procedural generation based on the coordinate system.
- The system of destruction, based on the substitution of objects, objects from the pool of static objects.
- Artificial intelligence, with a primitive simulation of life and interaction with each other, the world and the player.
- Game controller, with a set of weapons and skills.

In the folder [Infinity-Square-Space/Version 1.0/Compiled Builds/](https://github.com/nvjob/Infinity-Square-Space/tree/master/Version%201.0/Compiled%20Builds) are compiled builds of the game, under different API DirectX 11, DirectX 12 and Vulkan.

------------------------------------

### Prerequisites

To work on the project, you will need a Unity version of at least 2018.3.9f1 (64-bit).

### Installation:
- Create a new project in Unity.
- Download from the folder [Infinity-Square-Space/Version 1.0/Sources/](https://github.com/nvjob/Infinity-Square-Space/tree/master/Version%201.0/Sources) archive Assets.zip, ProjectSettings.zip and unpack them in the directory of your new project.
- Open the desired scene in the Scenes directory.

![GitHub Logo](https://lh3.googleusercontent.com/iqP50GVSEsAmx2Z-_F30-T7g8y4cxuVRkMTX6QcEd1ZYmXoI415QodXWSH4gb5guusteJGdQHzEwldW8KLfZSV7l2VdvOflkWMi_3lALtn8pTgg5hgtD1ADxTrLpxaWZ7c61Q44yAq9rlIpNhRsg_XJy78qsHi4-4KG09NJjbXkrKMGGj20nOVtxecoik7rmV3Ti-WlschE4lra3x3Twy1vgOVQ-l-Cm_sgYXv_2adxX-YYe1dNE8CfjO4stv4nvPysTQ2NMQuVkLw3gdPq6USpTSvjdteqC8oG6toqgFgADYQf9_kNwg5qWOtm5XA48VLNr3D84qU2oeKLRK89vwiUidndQU4ttn3zVLRvbOpCXlp4nbsnZ8mMXs1MVlEM_RFTccVOYDQOyiyt1LtpH7YrPxIddeI4QVROk8TLwM2XmxhfBFfwETy_tRObJ_saki83YgHdWQy3-NnpuFZw4AtAYqst8e70YlGSqSMCnslbOyYXFp5yotqXE9COXl2tjOhHiegngIIkLwqSXWMUloOtuBzfT4Z1sFwv1Gs-bo-QVA_oqM8qGVXQBxJcvyywb2rEOIJHYd7gEFjE2jNXjDUuBV4w2rB3b4ZQ7LZ195QdF1HxRNsA51d0_Fe7ykJhpyozw_GjGBXTSjSM6KlPKBoB5APaEKWeaN0GHBdvwxSW_JzxQPy65sBmsGzK3R8gCimmeEqE8xdSfwHp-nvezpC9G=w377-h163-no) ![GitHub Logo](https://lh3.googleusercontent.com/5ADpi0JbT2X_uFN4aIBl4T9chy9c8Qc9tlfa6I1mp-lK95gKAQ49wNz8OwjBVP-b4bz5V9PycqWgqxB-sLRzmQ2HxEFFZ5_1uSo1r3PD4LWA0icCX6LHC_MHQVxpkS9cpjamXQnLPIG1xTskVHk_OCaEIdypqltrbwkerAWhCSKKFdCLJv3xyMzCbLxtHxLA58TRmmZ3CXeIJxDHXOhgjKV5M8RiBOB9f20Xb0tFzvVv2X8QLwLObJ9VGXytKZRh05MX0fLCow5UtFCyQi8pJM-IjRezopYOfs2LYqumABKZY6t5926lQtr-XM7tfJ-W3YNCcmqpsrhDrMUpg4122LVsJdLf7FpkvClyWmZi-u8uTKxQPKniAvLpb0sV-MD2UU5TncZcZ0WFBccyYvzMcCiYriHN9EoAXsDoPZX5fusTZo33-6SjDa7Mjz2R80RIXlVHFPqUyZgmDnAW0nPGVN-xl9sq93HTMEcbEhoKeqgMKTmLYbTDrPQv-KzFEE86sf4qe752hlo6Eh69-QSI84PPl8O1AS2Vqdr6l0bsZInXCvlZl3gnqz8SxG3MYR9k2uQp8r2XNp4_dxV6oB7WrRsyWYdB7RKwJLrqA3Fn78rRXHcDaGzh7AbzekVhWvY-Li3IF2X6JXQUcv6J0qcWZUTzmG0KUUbCI3cS8-n3CfY_7oBncixVxreg3i3FbcxpXHK2zkgWww767y3r6wx24R3r=w352-h135-no)
![GitHub Logo](https://lh3.googleusercontent.com/U_zoiCWIocCXmlUknEW_5Pmvq8tb_aLFm3OAEsyjEmEHVnVsahyf5M2-3XxmyOJmEiKTo1uncbTPCPqyysA4_XW5VWRQOWWD2NYjuobBx-I4ti0D0APMYXh73pjMeKguMuRQXXKj_rXwnTvViB8WZxSyNA9P9PsxvwmVfGbIRxtK1Kso6xRUAlQLg_36c1VhQqMOEV-mgZzzwm-6BIAsRS1Fc-WxjiUhyrFDeNEzVu3uF8jqgSffJvQDDOo_BlgQTWSidlltfIOhwxtN1oytsw99pwZcDVWWvJAjbLR9pIK8zuxx6SDwTYkS83Gjh0wahZnJPFBPU1TcGuEh3r_qFaCC1x8Z0DzgYtpJ9C5_FV6btugng6ymLkIu0025b6ydI7dZrNztW_sXusY5QnGjpcoDaC5oNPjMxl1_899OW2fbGX6WiuYvDtyH9U_NyVqYvm4rXsNq4ZGyIBr2jDS2hTtvHSFAdTA6bz7r6C3xonDtjmpOO5yUQ6j1NcGSNLVIAIa-14dc94gRJ8Rh817qbaLROu_8d6gPCE_bAokOmkTfFguSqrsg9K-y5nozkjJ338No1ASM_eqgoFdyrEDrAoqVBHT718U4-9N49KMkyf0IYQuBxPS1bK-f3FsULQPOc-0TsfXdLxfGt3H-ZCOAsx6guS7dQMMtxmLUykVP4KFV-8TshXj2QiY38xqhpa7sPn-I48tYkOqi6FEiTfO3F7Js=w462-h150-no)

------------------------------------

### Authors
Nicholas Veselov - Development and design. https://nvjob.pro

### License
This project is licensed under the MIT License - see the LICENSE file for details
