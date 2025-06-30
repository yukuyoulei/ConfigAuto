# ConfigAuto

## Stargazers over time
[![Stargazers over time](https://starchart.cc/yukuyoulei/ConfigAuto.svg?variant=light)](https://starchart.cc/yukuyoulei/ConfigAuto)

## 概述 (Overview)

ConfigAuto 是一个基于 C# 代码热更新方案（例如 ILRuntime 或 HybridCLR）的 Unity 编辑器扩展工具。它允许开发者通过在 `Editor` 目录中配置 C# 匿名类，自动生成对应的强类型配置类并将数据直接烘焙到代码中，从而**彻底免除**了运行时的序列化与反序列化开销。

## 特性 (Features)

ConfigAuto 提供以下显著的技术特性，旨在优化您的游戏开发流程：

*   **纯 C# 代码输出 (Pure C# Code Output)**：
    *   配置数据直接转换为结构清晰、易于理解和使用的 C# 代码。
*   **极致性能 (Exceptional Performance)**：
    *   **零 GC (Zero Garbage Collection)**：配置数据在编译期即被处理为静态数据或通过高效方式访问，运行时不产生垃圾回收负担。
    *   **零运行时 I/O (Zero Runtime I/O)**：所有配置数据在游戏启动前已集成到代码中，运行时无需进行文件读取操作。
    *   **无序列化/反序列化开销 (No Serialization/Deserialization Cost)**：数据直接在内存中以其原生 C# 类型存在，彻底告别了 JSON、XML、Protobuf 等格式的解析和转换成本。
*   **懒加载支持 (Lazy Loading Support)**：
    *   生成的配置类可以设计为按需加载，即仅在首次访问特定配置数据时才进行初始化，有助于优化启动时间和内存占用，尤其适用于大型配置。
*   **广泛的平台兼容性 (Broad Platform Compatibility)**：
    *   生成的 C# 代码具有天然的跨平台性，完美适用于 PC、Mac、Linux、Android、iOS、WebGL 以及包括微信小游戏在内的各种小游戏平台。
*   **低学习门槛 (Low Learning Curve)**：
    *   基于简单的匿名类配置和自动化的代码生成，开发者和策划都能快速上手。

## 工作原理 (How it Works)

ConfigAuto 的核心机制旨在简化和加速游戏配置数据的处理流程，其工作步骤如下：

1.  **配置文件发现 (Configuration File Discovery)**：
    *   ConfigAuto 会在 Unity 编辑器环境下，自动扫描项目 `Editor` 目录（及其子目录）下所有命名以 `Config_` 开头的 C# 文件（例如 `Config_PlayerData.cs`）。这些文件被视为配置定义文件。

2.  **匿名类定义结构 (Defining Structure with Anonymous Classes)**：
    *   在这些 `Config_` 文件中，开发者通过 C# 的匿名类来定义配置数据的结构。匿名类提供了一种简洁的方式来声明配置项的字段名和数据类型，而无需创建显式的、完整的类定义。
    *   例如，一个 `Config_PlayerData.cs` 文件可能包含类似 `new { ID = 1, Name = "Player1", Level = 10 }` 这样的匿名类实例，用于定义玩家数据的结构和默认值或示例数据。

3.  **代码生成 (Code Generation)**：
    *   一旦找到并解析了这些配置文件，ConfigAuto 会根据匿名类定义的结构，在编译时自动生成对应的强类型 C# 类。
    *   这些生成的类会包含与匿名类中定义的字段相匹配的属性。
    *   生成的代码会被放置在项目根目录下（与 `Assets` 目录同级）的 `ConfigAuto/Configs/` 文件夹中。例如，`Config_PlayerData.cs` 会生成一个 `PlayerData.cs`（或其他类似命名，具体取决于 `ConfigGen.cs` 的实现）的类文件。

4.  **数据填充与使用 (Data Population and Usage)**：
    *   生成的 C# 类会直接包含配置文件中定义的数据。这意味着数据在编译时就被硬编码到类中，或者通过生成的代码逻辑进行初始化。
    *   在游戏运行时，可以直接访问这些生成的类的静态属性或方法来获取配置数据，无需进行任何文件读取、解析或反序列化操作。

5.  **核心优势详解 (Detailed Core Benefits)**：
    *   **杜绝反射与序列化瓶颈 (Eliminate Reflection and Serialization Bottlenecks)**：配置数据在编译期便已融入代码，从根本上消除了运行时的反射调用以及对 JSON、XML、二进制等格式进行序列化和反序列化的需求，从而极大地提升了数据访问速度与整体性能。
    *   **类型安全保障 (Type Safety Guarantee)**：生成的代码是强类型的，有助于在编码阶段即发现潜在错误，有效减少运行时因数据类型不匹配或配置结构错误导致的问题。
    *   **无缝集成与热更新兼容 (Seamless Integration and Hot-Update Compatibility)**：生成的 C# 代码可以像其他游戏逻辑代码一样被直接引用和管理。对于支持代码热更新的框架（如 ILRuntime 或 HybridCLR），这些生成的配置类也可以被正常纳入热更新流程，保持了开发与部署的灵活性。
    *   **简化策划与开发协作 (Simplified Planner-Developer Collaboration)**：策划可以直接修改 `Editor` 目录下的 `Config_` 文件中的匿名类定义（或通过配套工具间接修改），开发者在 Unity 编辑器刷新后，新的配置代码即会自动生成，显著简化了配置的更新与迭代流程。

## 快速入门/用法 (Getting Started / Usage)

以下步骤将引导您快速开始使用 ConfigAuto：

1.  **获取并放置 `ConfigGen.cs` 脚本**：
    *   将核心脚本 `ConfigGen.cs` 放置到您 Unity 项目中任意一个 `Editor` 文件夹下。例如，您可以创建一个路径 `Assets/Editor/ConfigAuto/ConfigGen.cs`。

2.  **创建您的第一个配置文件**：
    *   **命名规范**：在您的 Unity 项目的 `Editor` 文件夹下的任意位置（或其子文件夹中），创建一个新的 C# 脚本。该脚本的名称必须以 `Config_` 作为前缀，例如 `Config_GameSettings.cs` 或 `Config_MonsterData.cs`。
    *   **定义配置结构与数据**：在此文件中，使用 C# 匿名类来定义您的配置。以下是一个简单的示例 `Config_GameSettings.cs`：
        ```csharp
        // Assets/Editor/Configs/Config_GameSettings.cs
        // 注意：此文件本身不应包含 `class` 或 `namespace` 定义；
        // 它只需要包含一个或多个匿名类的实例化表达式。
        // ConfigAuto 会读取这些表达式来生成实际的配置类。

        new 
        {
            GameName = "我的神奇游戏",
            Version = "1.0.0",
            MaxPlayers = 4,
            InitialHealth = 100,
            UnlockAllLevels = false
        }

        // 如果需要定义多组数据或者列表形式的配置，可以参考如下方式：
        // (假设这是 Config_MonsterData.cs)
        /*
        new []
        {
            new { MonsterId = 1, Name = "史莱姆", Hp = 50, Attack = 5 },
            new { MonsterId = 2, Name = "哥布林", Hp = 100, Attack = 10 },
            new { MonsterId = 3, Name = "巨龙", Hp = 1000, Attack = 50 }
        }
        */
        ```

3.  **自动代码生成**：
    *   当 `ConfigGen.cs` 脚本被放置在 `Editor` 目录下，并且您创建了符合命名规范的配置文件后，ConfigAuto 会在 Unity 编辑器启动、脚本重新编译或您对配置文件进行修改并保存后自动运行。
    *   它会查找所有 `Config_` 开头的配置文件，并根据其内容生成相应的 C# 类。

4.  **查找生成的代码**：
    *   生成的配置类文件会自动存放在项目根目录下的 `ConfigAuto/Configs/` 文件夹中（即与 `Assets` 文件夹同级）。
    *   例如，如果您创建了 `Config_GameSettings.cs`，那么通常会生成一个名为 `GameSettings.cs` (或其他类似名称，具体命名逻辑取决于 `ConfigGen.cs` 的实现) 的文件在此目录下。

5.  **在运行时访问配置数据**：
    *   生成的类通常会包含静态属性或方法，以便在游戏运行时方便地访问配置数据。假设 `Config_GameSettings.cs` 生成了 `GameSettings` 类，您可以这样访问数据：
        ```csharp
        // 在您的游戏逻辑脚本中 (例如：Assets/Scripts/MyGameManager.cs)
        using UnityEngine; // 如果需要 MonoBehaviour
        // 通常不需要显式 using 生成的配置命名空间（如果有的话），
        // 因为它们可能在全局命名空间中，或具体取决于 ConfigGen.cs 的实现。

        public class MyGameManager : MonoBehaviour
        {
            void Start()
            {
                // 假设生成的 GameSettings 类有一个静态实例或静态属性来访问数据。
                // 以下为示意代码，实际访问方式取决于 ConfigGen.cs 的生成逻辑。
                Debug.Log("游戏名称: " + GameSettings.Data.GameName);
                Debug.Log("版本: " + GameSettings.Data.Version);
                Debug.Log("最大玩家数: " + GameSettings.Data.MaxPlayers);

                if (GameSettings.Data.UnlockAllLevels)
                {
                    Debug.Log("所有关卡已解锁!");
                }
            }
        }
        ```
    *   **重要提示 (Important Note)**：具体的访问方式（例如类名、属性名、是否需要 `.Data` 结构等）完全取决于 `ConfigGen.cs` 脚本内部是如何设计生成代码的。请务必参考 `ConfigGen.cs` 的具体实现细节或其维护者提供的文档（如果有）来了解准确的用法。对于列表形式的配置（如 `Config_MonsterData.cs` 的例子），生成的代码通常会提供一个列表（List）或字典（Dictionary）等集合类型来访问各个条目。

## 优势 (Benefits)

采用 ConfigAuto 将为您的项目团队带来多方面的显著优势：

*   **极致的运行时性能 (Ultimate Runtime Performance)**：
    *   通过消除 GC（垃圾回收）、I/O（文件读写）及序列化/反序列化过程的开销，ConfigAuto 确保了配置数据访问的最高效率，这对游戏性能敏感的模块尤为关键。
*   **简化的团队协作流程 (Simplified Team Workflow)**：
    *   **策划驱动配置 (Planner-Driven Configuration)**：“需求不怕复杂，配置表能配出来功能就能做出来。” 系统策划能够直接通过定义匿名类（或使用配套工具）来精确表达配置结构和数据，数值策划则专注于填充具体数值。
    *   **配置即文档 (Configuration as Documentation)**：“配置表是最直接客观的。” 生成的配置代码结构清晰，字段明确，其本身就成为了一种准确、易于查阅的“活文档”，显著减少了团队沟通成本和潜在误解。
    *   **开发者无缝集成 (Seamless Developer Integration)**：开发者可以直接在代码中通过强类型访问配置数据，无需关心数据来源和解析过程，从而提升了开发效率和代码的健壮性。
*   **高度的灵活性与可扩展性 (High Flexibility and Scalability)**：
    *   轻松应对复杂需求和大型配置表，保持开发过程的敏捷性。
    *   与主流 C# 代码热更新框架（如 ILRuntime、HybridCLR 等）良好兼容，使得配置的更新也能享受热更新带来的便利，无需重新打包应用程序。
*   **提升开发效率与准确性 (Increased Development Efficiency and Accuracy)**：
    *   自动代码生成极大地减少了手动编写和维护配置解析代码的重复性劳动。
    *   编译期的强类型检查能在早期发现潜在错误，避免了许多在运行时才可能暴露的数据相关 bug。
    *   “表里有的字段全用上，大概率就不会有偏差了。” 这一理念确保了配置数据在代码中的全面和正确使用。
*   **赋能策划，解放程序 (Empowering Planners, Freeing Programmers)**：
    *   让策划更深入地参与到游戏功能的实现与迭代中，程序开发者则可以更专注于核心游戏逻辑和技术挑战，而非繁琐的配置数据接入与管理。
    *   引用其经典设计理念：“如果说传统配置表是低魔世界的走路的话，luban是武侠世界的轻功，而ConfigAuto方案就是在蓝大和walon创造的高魔世界里的筋斗云，可以随心所欲，想去哪儿转念即到。” 这生动形象地说明了 ConfigAuto 在配置处理上所能达到的自由度与极致效率。
