1. Generate a private key for the CA using the following command:
```
openssl genpkey -algorithm RSA -out ca.key
```

2. Generate a self-signed certificate for the CA using the following command:
This command will create a self-signed CA certificate named ca.crt.
```
openssl req -new -x509 -key ca.key -out ca.crt -subj "/C=AU/ST=Vic/L=Melbourne/O=Mtls Demo Server/CN=mtlsdemo.server"
```

3. Create a new serial file for the CA using the following command:
```
echo 1000 > ca.srl
```

4. Generate a private key for the server using the following command:
```
openssl genpkey -algorithm RSA -out server.key
```

5. Generate a certificate signing request (CSR) for the server using the following command:
```
openssl req -new -key server.key -out server.csr -subj "/CN=localhost"
```

6. Sign the server CSR using the CA certificate and private key using the following command:
```
openssl x509 -req -in server.csr -CA ca.crt -CAkey ca.key -out server.crt -days 365 -sha256 -CAcreateserial
```

7. Combine the server certificate, private key, and CA certificate into a PFX file using the following command:
```
openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt -certfile ca.crt --passout pass:Password01
```
