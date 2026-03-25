#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include "udpreceiver.h"
int main(int argc, char *argv[])
{
    QGuiApplication app(argc, argv);
    qmlRegisterType<UdpReceiver> ("DroneApp",1,0,"UdpReceiver");
    QQmlApplicationEngine engine;
    QObject::connect(
        &engine,
        &QQmlApplicationEngine::objectCreationFailed,
        &app,
        []() { QCoreApplication::exit(-1); },
        Qt::QueuedConnection);
    engine.loadFromModule("Tracking", "Main");

    return app.exec();
}
