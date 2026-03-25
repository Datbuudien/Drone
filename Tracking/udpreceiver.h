#ifndef UDPRECEIVER_H
#define UDPRECEIVER_H
#include<QObject>
#include<QUdpSocket>
class UdpReceiver : public QObject{
    Q_OBJECT
    Q_PROPERTY(QString coordinate READ coordinate NOTIFY coordinateChange)
public:
    explicit UdpReceiver(QObject *parent =nullptr );
    QString coordinate() const;
signals:
    void coordinateChange();
private slots:
    void readPendingDatagrams();
private:
    QUdpSocket *udpSocket;
    QString m_coordinate;
};

#endif // UDPRECEIVER_H
