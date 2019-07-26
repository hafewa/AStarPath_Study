using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPath : MonoBehaviour
{
    private Grid grid;

    // Use this for initialization
    void Start()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        grid.player.name = string.Format("PlayerA-{0}-{1}", grid.getItem(grid.player.position).x, grid.getItem(grid.player.position).y);
        grid.destPos.name = string.Format("PlayerB-{0}-{1}", grid.getItem(grid.destPos.position).x, grid.getItem(grid.destPos.position).y);
        FindingPath(grid.player.position, grid.destPos.position);
    }

    // A*寻路
    void FindingPath(Vector3 s, Vector3 e)
    {
        Grid.NodeItem startNode = grid.getItem(s);
        Grid.NodeItem endNode = grid.getItem(e);

        List<Grid.NodeItem> openSet = new List<Grid.NodeItem>();   // 开放集合：用来存放所有被考虑最短路径的节点
        HashSet<Grid.NodeItem> closeSet = new HashSet<Grid.NodeItem>();    // 封闭集合：用来存放已经判定过最短路径的节点
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Grid.NodeItem curNode = openSet[0];

            // 这一步主要从开放列表中选取fCost及hCost最小的最优点
            for (int i = 0, max = openSet.Count; i < max; i++)
            {
                if (openSet[i].fCost <= curNode.fCost &&
                    openSet[i].hCost < curNode.hCost)
                {
                    curNode = openSet[i];
                }
            }

            openSet.Remove(curNode);
            closeSet.Add(curNode);

            // 找到的目标节点，回溯路径，退出
            if (curNode == endNode)
            {
                generatePath(startNode, endNode);
                return;
            }

            // 判断周围相邻节点，相邻节点如果不在开放列表中，加入开放列表，设置父节点为当前节点；
            // 相邻节点的gCost如果比经过当前节点计算的gCost大，更新这个相邻节点的gCost，并重新计算hCost，设置父节点为当前节点，如果不在开放列表中，加入开饭列表；
            // 主要是刷新相邻节点的gCost、hCost、父节点以及开放列表
            List<Grid.NodeItem> nearNodes = grid.getNeibourhood(curNode);
            foreach (var item in nearNodes)
            {
                // 如果是墙或者已经在封闭列表中
                if (item.isWall || closeSet.Contains(item))
                    continue;
                // 计算当前相领节点经当前节点与开始节点距离gCost
                int newCost = curNode.gCost + getDistanceNodes(curNode, item);
                // 如果距离更小，或者原来不在开放列表中
                if (newCost < item.gCost || !openSet.Contains(item))
                {
                    // 更新与开始节点的距离
                    item.gCost = newCost;
                    // 更新与终点节点的距离
                    item.hCost = getDistanceNodes(item, endNode);
                    // 更新父节点为当前选定的节点
                    item.parent = curNode;
                    // 如果节点不在开放列表中，将它加入开放列表中
                    if (!openSet.Contains(item))
                    {
                        openSet.Add(item);
                    }
                }
            }
        }

        generatePath(startNode, null);
    }

    // 生成路径
    void generatePath(Grid.NodeItem startNode, Grid.NodeItem endNode)
    {
        List<Grid.NodeItem> path = new List<Grid.NodeItem>();
        if (endNode != null)
        {
            Grid.NodeItem temp = endNode;
            while (temp != startNode)
            {
                path.Add(temp);
                temp = temp.parent;
            }
            // 反转路径
            path.Reverse();
        }
        // 更新路径
        grid.updatePath(path);
    }

    // 获取两个节点之间的距离【对角线估价法】
    int getDistanceNodes(Grid.NodeItem a, Grid.NodeItem b)
    {
        int cntX = Mathf.Abs(a.x - b.x);
        int cntY = Mathf.Abs(a.y - b.y);
        // 判断到底是那个轴相差的距离更远
        if (cntX > cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }
    }


}
