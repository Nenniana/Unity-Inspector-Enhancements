# Unity Inspector Enhancements

Inspector Enhancements is a Unity package offering custom drawers and property attributes to enhance the Unity Inspector experience. This collection of utility attributes improves data visualization, organization, and validation in the editor.

## Features

### **HideIf / ShowIf Attributes**
The `[HideIf]` and `[ShowIf]` attributes allow conditional visibility of specific fields in the Inspector. They can be configured to dynamically hide or display fields based on certain conditions, enhancing data organization and readability.

   - **Capabilities**:
     - Supports `bool` fields and methods that return `bool` values.
     - Accepts parameters if a method is provided, enabling custom logic.
     - Can operate without parameters, hiding or showing based on the field’s null status.

   - **Limitations**: Not compatible with custom structs or classes; supports Unity native objects and primitive types.

### **HideLabel Attribute**
The `[HideLabel]` attribute simply hides the label of a field in the Inspector. This is useful for a cleaner display, removing labels where they aren’t necessary and simplifying the user interface.

   - **Capabilities**: Provides a cleaner look by removing labels where they aren’t needed, which can simplify the interface.

### **Inline Property Attribute**
The `[InlineProperty]` attribute inlines a serializable class or struct, displaying it directly within the Inspector. This attribute is particularly useful for visualizing nested data structures, like other classes within a `MonoBehaviour`, and allows for flexible customization.

   - **Capabilities**:
     - Inlines a custom class or struct as a field, showing each associated field directly.
     - Allows for custom naming, with optional name prefixing for each field in the inline class.
     - Supports customization of both the class setup and individual field display, ideal for organizing nested data within `MonoBehaviour` scripts.

### **Required Attribute**
The `[Required]` attribute ensures that a field cannot be left as `null`, helping to prevent common runtime errors by flagging fields that require a value assignment.

   - **Capabilities**: Enforces that an object or field must be assigned by displaying a warning if left unfilled.