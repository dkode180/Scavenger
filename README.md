# Desarrollo SCAVENGER

## Problemas de desarrollo con el motor de Unity
- ### Segunda semana de abril

Al intentar descargar el motor Unity para desarrollar el juego, el sistema mostraba **pantallazos azules (BSOD)** con el error:

> **"page fault in nonpaged area"**

### Pruebas y ajustes realizados:

- Inicié el ordenador en **modo recovery** para evitar los pantallazos.
- Cambié el comportamiento de la **gestión de la memoria virtual** del dispositivo.
- Desactivé el **reinicio automático** después del BSOD.
- **Optimicé el disco duro** para disminuir la probabilidad de errores.
- **Testeé la memoria RAM** en busca de errores.
- Hice varias pruebas adicionales sin éxito.

#### Videos de apoyo utilizados:

- [Cómo solucionar BSOD - Video 1](https://www.youtube.com/watch?v=1cJAVryIKjs)
- [Cómo solucionar BSOD - Video 2](https://www.youtube.com/watch?v=M13ntyIlW1o)
- [Cómo solucionar BSOD - Video 3](https://www.youtube.com/watch?v=d6O0H0xO1jE&t=180s)

> Finalmente decidí cambiar a un motor que no diera este error. Así fue como pasé a trabajar con **Godot Engine**.

---

## Proyectos Pre-desarrollo y Pre-documentación
- ### Segunda y Tercera semana de abril

Para familiarizarme con el motor Godot, investigué documentación y seguí algunos tutoriales clave:

- Seguí el tutorial **"How to make a Video Game - Godot Beginner Tutorial"** del canal **Brackeys**, con el que creé una especie de Super Mario.
- Consulté la **documentación oficial de Godot**, sobre todo el apartado de **colliders**.
- Revisé por encima el tutorial **"Godot 4 Projectiles Tutorial"** del canal **Gwizz**, para entender cómo programar y gestionar proyectiles (lo estudiaré más a fondo más adelante).

### Bibliografía:

- [Brackeys - Godot Beginner Tutorial](https://www.youtube.com/watch?v=LOhfqjmasi0&t=3186s)
- [Documentación oficial - Colliders](https://docs.godotengine.org/es/4.x/tutorials/physics/collision_shapes_2d.html)
- [Gwizz - Projectiles Tutorial](https://www.youtube.com/watch?v=YPvPqOqbx-I)

---

## Desarrollo del Proyecto

Hasta ahora, he completado las siguientes tareas:
### Cuarta semana de abril
- Recopilación de **efectos de sonido**, **sprites** y otros elementos visuales del juego.
- Creación de la **arena** donde se desarrollará la partida.
- Programación del **movimiento de la nave**.
- Implementación del **contador de porcentaje de disparo** del juego.

### Primera semana de mayo
- Creación de la clase Enemy, que controla el comportamiento de las naves enemigas
- Creacion del nodo Timer que gestiona donde y cuándo spawnean los enemigos
### Segunda semana de mayo
- Creación de la clase Bullet e implementación de la misma en el juego (gestion de colliders, sprites, etc)
- Gestión a la hora de eliminar tanto los enemigos cuando entran en contacto con una bala como la misma bala al impactar con cualquier collider
- Creación de la lógica de disparo del juego (El tema del porcentaje de disparo certero)
- Gestión del GAME OVER (en proceso) 
