 using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO.Ports;

    public class ArduinoController : MonoBehaviour
    {
        SerialPort serialPort = new SerialPort("COM19", 9600); //Coloque a porta do seu arduino na string.

        void Start()
        {
            //Abrir porta
            serialPort.Open();

            serialPort.ReadTimeout = 100;
        }

        void Update()
        {
            //Se a porta estiver aberta
            if (serialPort.IsOpen)
            {
                try
                {
                    //Receber carta de amor do Arduino.
                    string message = serialPort.ReadLine();
                    Debug.Log("Recebido: " + message);
                    ProcessArduinoMessage(message);
                }
                catch (System.Exception)
                {
                    // Ignora timeouts
                }

                    //Enviar carta de amor pra Arduino.
                if (Input.GetKeyDown(KeyCode.A))
                {
                    SendCommandToArduino('A');
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    SendCommandToArduino('D');
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    SendCommandToArduino('W');
                }
                 else if (Input.GetKey(KeyCode.S))
                {
                    SendCommandToArduino('S');
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