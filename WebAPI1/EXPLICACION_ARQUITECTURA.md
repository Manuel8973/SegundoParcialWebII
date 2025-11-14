# ¿Por qué elegí esta arquitectura?

Elegí una **arquitectura de capas** porque es como organizar tu casa: cada cosa tiene su lugar. Los **controladores** son la puerta de entrada (reciben las peticiones HTTP), los **servicios** son donde está la lógica del negocio (las reglas de cómo funcionan las tareas, miembros y prioridades), y el **DbContext** es donde guardamos los datos. De esta forma, si necesito cambiar algo de la base de datos, no tengo que tocar el código de los servicios ni de los controladores. Además, es más fácil de probar y mantener porque cada parte hace solo una cosa específica.

En resumen: separé las responsabilidades para que el código sea más ordenado, fácil de entender y de modificar cuando sea necesario.
