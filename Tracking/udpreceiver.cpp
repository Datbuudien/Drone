#include"udpreceiver.h"
#include<QNetworkDatagram>
UdpReceiver::UdpReceiver(QObject *parent): QObject(parent),m_coordinate("Wating to Unity response...") {
    udpSocket = new QUdpSocket(this);
    udpSocket->bind(QHostAddress::AnyIPv4,5000);
    connect(udpSocket,&QUdpSocket::readyRead,this,&UdpReceiver::readPendingDatagrams);
}
QString UdpReceiver::coordinate() const{
    return m_coordinate;
}
void UdpReceiver::readPendingDatagrams(){
    while(udpSocket->hasPendingDatagrams()){
        QNetworkDatagram datagram = udpSocket->receiveDatagram();
        m_coordinate = QString::fromUtf8(datagram.data());
        emit coordinateChange();
    }
}
