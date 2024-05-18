# 🌟 Unity-Arduino Guia Definitivo 🌟

Bem-vindo ao **Unity-Arduino Guia Definitivo**! Este repositório oferece um guia completo para configurar e estabelecer comunicação entre o Arduino e a Unity. 🌐🚀

## 📋 Índice

- [Introdução](#introdução)
- [Pré-requisitos](#pré-requisitos)
- [Configuração do Arduino](#configuração-do-arduino)
- [Configuração da Unity](#configuração-da-unity)
- [Testando a Comunicação](#testando-a-comunicação)
- [Contribuições](#contribuições)
- [Licença](#licença)

## 🌟 Introdução

Este guia fornece instruções passo a passo para configurar a comunicação entre Arduino e Unity, permitindo que você crie projetos interativos incríveis. Com este guia, você aprenderá a:

- Configurar o Arduino para enviar e receber comandos.
- Configurar a Unity para se comunicar com o Arduino.
- Integrar ambos para criar interações dinâmicas.

## 🛠️ Pré-requisitos

Antes de começar, você precisará dos seguintes itens:

- [Arduino IDE](https://www.arduino.cc/en/software)
- [Unity](https://unity.com/)
- Na Unity entra em ***Project Settings/Player/Other Settings e altere o .NET STandard pelo .NET Framework***
- Placa Arduino (por exemplo, Arduino Uno)
- Cabo USB para Arduino

## ⚙️ Configuração do Arduino

1. **Escreva o código Arduino:**

    ```c
    int led = 13;
    int x = 0;

    int button = 3;
    int sinal_button = 0;

    int button1 = 8;
    int sinal_button1 = 0;

    char comando;

    void setup() {
      Serial.begin(9600);
      pinMode(led, OUTPUT);
      pinMode(button, INPUT_PULLUP);
      pinMode(button1, INPUT_PULLUP);
    }

    void loop() {
      sinal_button = digitalRead(button);
      sinal_button1 = digitalRead(button1);

      if (sinal_button == LOW && sinal_button1 == HIGH) {
        Serial.println("botao01Pressionado");
        digitalWrite(led, HIGH);
      }
      else if (sinal_button1 == LOW && sinal_button == HIGH) {
        Serial.println("botao02Pressionado");
        digitalWrite(led, HIGH);
      }

      if (Serial.available() > 0) {
        comando = Serial.read();
        switch (comando) {
          case 'A':
            piscarLedA();
            break;
          case 'B':
            piscarLedB();
            break;
          case 'C':
          case 'D':
          case 'E':
          case 'F':
          case 'G':
          case 'H':
          case 'I':
          case 'J':
          case 'K':
          case 'L':
            piscarLedCtoL();
            break;
        }
      }
    }

    void piscarLedA() {
      digitalWrite(led, HIGH);
      delay(10);
      digitalWrite(led, LOW);
      delay(3);
    }

    void piscarLedB() {
      digitalWrite(led, HIGH);
      delay(16);
      digitalWrite(led, LOW);
      delay(8);
    }

    void piscarLedCtoL() {
      digitalWrite(led, HIGH);
      delay(8);
      digitalWrite(led, LOW);
      delay(16);
    }
    ```

2. **Carregue o código na sua placa Arduino:**
   - Conecte sua placa ao computador e use o Arduino IDE para carregar o código.

## 🎮 Configuração da Unity

1. **Criar um novo projeto no Unity:**
   - Abra o Unity e crie um novo projeto.

2. **Adicionar o Script de Comunicação Serial:**

    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO.Ports;

    public class ArduinoController : MonoBehaviour
    {
        SerialPort serialPort = new SerialPort("COM3", 9600);

        void Start()
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
        }

        void Update()
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    Debug.Log("Recebido: " + message);
                    ProcessArduinoMessage(message);
                }
                catch (System.Exception)
                {
                    // Ignora timeouts
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    SendCommandToArduino('A');
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    SendCommandToArduino('B');
                }
                // Adicione mais comandos conforme necessário
            }
        }

        // Receber carta de amor do Arduino.
        void ProcessArduinoMessage(string message)
        {
            if (message == "botao01Pressionado")
            {
                Debug.Log("Botão 01 Pressionado");
            }
            else if (message == "botao02Pressionado")
            {
                Debug.Log("Botão 02 Pressionado");
            }
            else if (message == "botoesNaoPressionados")
            {
                Debug.Log("Nenhum botão pressionado");
            }
        }


        // Enviar carta de amor para arduino.
        public void SendCommandToArduino(char command)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(command.ToString());
                Debug.Log("Enviado: " + command);
            }
        }

        // Fechar a porta quando encerrar o app.
        void OnApplicationQuit()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
    ```

3. **Configurar a Porta Serial:**
   - Certifique-se de que a porta serial no script Unity (`"COM3"`) corresponda à porta do seu Arduino.

4. **Adicionar Componentes no Unity:**
   - Adicione `ArduinoController` a um `Empty GameObject`.

## ✅ Testando a Comunicação

1. **Rodar o Projeto Unity:**
   - Execute o projeto e pressione as teclas configuradas para enviar comandos ao Arduino.

2. **Verificar o Monitor Serial do Arduino:**
   - Verifique se os comandos são recebidos corretamente e que o LED está piscando conforme esperado.

## 🤝 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e enviar pull requests.

## 📜 Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

Feito com ❤️ por [Elísio Massaqui]

## (Opcional) Configure a sua placa arduino juntamente com protoboard pra piscar LED aos receber dados de Unity (Componentes físicos)

1 - Prepare apenas uma LED.

2 - Prepare dois botões ou apenas 1.

3 - Agora faça a ligação com os ***fios*** (em qualquer lugar da ***protoboard***, onde você preferir), no caso do resistor acredito que não é necessário explicar, pra quem tem uma mínima expertise com isso, conecte:

- LED = Pin 13 (GND)
- Resistor
- Botão 1 = Pin 3 (GND)
- Botão 2 = Pin 8 (GND)

Não esquça que ***TR e RX*** na placa serve pra monitorar a entrada e saída de dados.
