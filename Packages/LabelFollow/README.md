# Unity UI Label Follow System

## 概述

这个项目展示了如何在Unity中创建一个UI标签，并使其在世界空间中跟随一个目标物体。标签会根据相机距离进行缩放。

## 文件列表

- `LabelFollower.cs`：用于使标签跟随目标物体，并根据相机距离进行缩放。
- `LabelController.cs`：用于创建和管理标签的静态管理器类。

## 使用步骤

1. **创建一个Canvas**：
   - 在Unity编辑器中，右键点击层级视图，选择`UI -> Canvas`创建一个Canvas。
   - 确保Canvas的`Render Mode`设置为`World Space`。

2. **创建一个标签预制件**：
   - 在Canvas下创建一个`UI -> Image`对象作为标签的背景。
   - 设置图片的样式。
   - 在Image对象下创建一个`UI -> Text`对象，作为标签的文本内容。
   - 设置文本的内容和样式。
   - 将包含Image和Text的标签对象拖动到项目窗口中以创建一个预制件，然后删除层级视图中的标签对象。

3. **创建LabelController**：
   - 在一个空的GameObject上添加`LabelController`脚本。
   - 在脚本的Inspector面板中，设置`Canvas`为包含标签的Canvas对象。

4. **使用LabelController创建标签**：
   - 你可以在其他脚本中使用`LabelController.CreateLabel`方法来创建标签。例如：
   
   ```csharp
   using UnityEngine;

   public class ExampleUsage : MonoBehaviour
   {
       public Transform target;
       public GameObject labelPrefab;

       void Start()
       {
           LabelController.CreateLabel(target, labelPrefab, new Vector3(0, 2, 0), 1.0f);
       }
   }