# T1. 2ona Convocatòria: Recull d’activitats

## 1.
a. TLS/HTTPS:

- Xifrat asimetric: clau publica/privada: S'usa durant el handshake inicial per autenticar servidors mitjançant certificats digitals i intercanviar claus de manera segura.  
- Xifrat simetric: AES, Un cop establerta la connexió, s'empra per xifrar el trànsit de dades amb claus efímeres, ja que és més eficient en recursos  
- Funcions hash: sha256, Garanteixen la integritat dels certificats i dels paquets transmesos

b. WPA2

- Xifrat simetric: AES, xifra les dades transmeses per l'aire amb una clau compartida entre el dispositiu i el punt d'accés  
- Funcions hash: sha1, Participen en el procés d'autenticació EAP per verificar integritat

c. Bitlocker/FileVault (encriptació de discs)

- Xifrat simetric: AES, encripta els blocs del disc dur amb una clau secreta emmagatzemada  al sistema o en un xip TPM.

d. Criptomonedes Blockchain (Bitcoin o ethereum)

- Funcions hash: sha256: Generen identificadors únics per a transaccions i blocs, i asseguren la integritat de la cadena mitjançant l'enllaç hash entre blocs  
- Xifrat asimetric: Usat per crear signatures digitals que verifiquen la propietat de les adreces i transaccions

## 2.

a. Composició d’una petició HTTP:  
   Línia de sol·licitud: Conté el verb HTTP (GET, POST, etc.), el camí del recurs i la versió del protocol.  
   Capçaleres: Informació addicional com el tipus de contingut, autenticació, etc.  
   Cos (body):  S’utilitza principalment en peticions POST, PUT, PATCH per enviar dades.

a. Composició d’una resposta HTTP:  
   Línia d’estat: Indica la versió del protocol, el codi d’estat (ex: 200, 404\) i el missatge.  
   Capçaleres: Metadades de la resposta (tipus de contingut, longitud, etc.).  
   Cos (body): Contingut retornat pel servidor (HTML, JSON, etc.).  
2. 

| Verb | Significat |
| :---- | :---- |
| GET | Sol·licita dades d’un recurs, sense modificar-lo. |
| POST | Envia dades al servidor per crear un nou recurs. |
| PUT | Actualitza completament un recurs existent. |
| PATCH | Actualitza parcialment un recurs existent. |
| DELETE | Elimina un recurs. |
| OPTIONS | Demana quines opcions i mètodes admet el servidor per a un recurs. |

c.   
1. Petició GET amb un Id i el recurs s’ha trobat  
Codi: 200 Ok  
Body: El recurs sol·licitat (ex: JSON amb les dades del recurs).  
Raó: El recurs existeix i es retorna correctament.

2. Petició DELETE, on la BBDD ha fallat  
Codi: 500 Internal Server Error  
Body: Un missatge d’error genèric  
Raó: L’error és al servidor i no es pot garantir l’eliminació.

3. Petició PATCH on el recurs a modificar no existeix  
Codi: 404 Not Found  
Body: Un missatge informant que el recurs no existeix  
Raó: No es pot modificar un recurs inexistent.

4. Acció de login amb dades incorrectes  
Codi: 401 Unauthorized  
Body: Un missatge d’error genèric (ex: { "error": "Credencials incorrectes." })  
Raó: Les credencials no són vàlides i no s’autoritza l’accés.

5. Petició POST amb un rol no permès  
Codi: 403 Forbidden  
Body: Un missatge d’error (ex: { "error": "No tens permisos per realitzar aquesta acció." })  
Raó: L’usuari està autenticat però no autoritzat per aquesta acció.

## 3.

| Característica | WebSocket | Socket convencional (TCP/UDP) |
| :---- | :---- | :---- |
| Protocol base | HTTP/1.1 o HTTP/2 amb actualització a WS/WSS | TCP o UDP directe |
| Encriptació | WSS (WebSocket Secure) amb TLS/SSL | TLS/SSL implementat manualment sobre TCP |
| Autenticació | Tokens (JWT) i validació d'origen (CORS) | Certificats X.509 o claus compartides |
| Control d'accés | Límits de connexió per IP i grandària de missatges | Firewalls i llistes de control d'accés (ACL) |
| Protecció contra atacs | Validació d'Origin per evitar CSWSH, Rate limiting | Segmentació de xarxa, Sistemes de detecció d'intrusions |
| Exemples d'ús | Aplicacions web en temps real (xats, jocs) | Connexions directes entre servidors o dispositius IoT |

## 8.

   **Errors:**

1. Mètode HTTP:

   S’utilitza \[HttpDelete("login")\] per a un login, però el login hauria de ser un POST, ja que implica l’enviament de dades sensibles (usuari i contrasenya) i no l’eliminació de recursos.

2. Comprovació d’usuari incorrecta i possible NullReferenceException:

   No es comprova si usuari és null després de la crida a \_userManager.FindByEmailAsync. Si l’usuari no existeix, accedir a usuari.Email llançarà una excepció.

3. No es comprova la contrasenya:

   El codi només compara el correu electrònic, però no valida la contrasenya de l’usuari.

4. Fuga d’informació sensible als logs:

   Es registra la contrasenya de l’usuari als logs (usuari.password). Mai s’ha de registrar informació sensible com contrasenyes.

5. Retorn de l’objecte usuari complet:

   Retornar l’objecte usuari pot exposar informació sensible. S’hauria de retornar només la informació estrictament necessària (ex: token JWT).

Codi Corregit:

```
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
{
    var usuari = await _userManager.FindByEmailAsync(user.Email);
    if (usuari == null)
        return Unauthorized("Usuari o contrasenya incorrectes");

    // Comprova la contrasenya utilitzant el gestor d'identitat
    var passwordValid = await _userManager.CheckPasswordAsync(usuari, user.Password);
    if (!passwordValid)
        return Unauthorized("Usuari o contrasenya incorrectes");

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuari.UserName),
        new Claim(ClaimTypes.NameIdentifier, usuari.Id.ToString())
    };

    // Només logueja informació no sensible
    _logger.Information("Usuari {UserName} amb id {UserId} ha fet login amb èxit!", usuari.UserName, usuari.Id);

    var token = CreateToken(claims.ToArray());
    return Ok(new { Token = token });
}
```

[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/Fe1Bd4y7)
